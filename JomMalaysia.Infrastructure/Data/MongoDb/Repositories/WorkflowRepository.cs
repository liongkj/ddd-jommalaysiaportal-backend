using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.WorkflowUseCase;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using JomMalaysia.Infrastructure.Data.MongoDb.Helpers;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.Exceptions;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<WorkflowDto> _db;

        public WorkflowRepository(IMongoDbContext context, IMapper mapper)
        {
            _db = context.Database.GetCollection<WorkflowDto>("Workflow");
            _mapper = mapper;
        }
        public async Task<NewWorkflowResponse> CreateWorkflowAsyncWithSession(Workflow workflow, IClientSessionHandle session)
        {

            WorkflowDto workflowDto;
            try
            {
                workflowDto = _mapper.Map<WorkflowDto>(workflow);
                await _db.InsertOneAsync(session, workflowDto);

            }
            catch (Exception e)
            {
                throw e;
            }
            return new NewWorkflowResponse(workflowDto.WorkflowId + " created", true);
        }

        public async Task<GetWorkflowResponse> GetWorkflowByIdAsync(string workflowId)
        {
            Workflow workflow;
            try
            {
                var query = await
                    _db.AsQueryable()
                    .Where(W => W.WorkflowId == workflowId)
                    .FirstOrDefaultAsync();
                workflow = _mapper.Map<Workflow>(query);
                var temp = ListingDtoParser.Converted(_mapper, query.Listing);
                if (temp != null)
                {
                    workflow.Listing = temp;
                }
            }
            catch (Exception e)
            {
                var Errors = new List<string>();
                Errors.Add(e.ToString());
                return new GetWorkflowResponse(Errors);
            }
            return workflow != null ? new GetWorkflowResponse(workflow, true) :
                new GetWorkflowResponse(new List<string> { "workflow not found" });
        }

        public async Task<GetAllWorkflowResponse> GetAllWorkflowByStatusAsync(WorkflowStatusEnum status, int counterpage = 10, int page = 0)
        {
            //todo add paging
            List<WorkflowDto> query = new List<WorkflowDto>();
            List<WorkflowViewModel> Workflows;
            try
            {
                if (status == null)
                {
                    return new GetAllWorkflowResponse(new List<string> { "Not a valid workflow status" }, false);
                }
                if (!status.Equals(WorkflowStatusEnum.All))
                {
                    query = await
                        _db.AsQueryable()
                        .Where(W => W.Status.Equals(status.ToString()))
                        .ToListAsync();
                }
                else
                {
                    query = await
                        _db.AsQueryable()
                        .ToListAsync();
                }
                Workflows = _mapper.Map<List<WorkflowViewModel>>(query);


            }
            catch (AutoMapperMappingException e)
            {
                throw new MappingException(e.InnerException.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            var response = Workflows.Count < 1 ?
                new GetAllWorkflowResponse(new List<string> { "No workflow found" }, false) :
                new GetAllWorkflowResponse(Workflows, true, $"{Workflows.Count} result found");
            return response;
        }


        public async Task<WorkflowActionResponse> UpdateAsync(Workflow updatedWorkflow, IClientSessionHandle session = null)
        {
            ReplaceOneResult result;

            FilterDefinition<WorkflowDto> filter = Builders<WorkflowDto>.Filter.Eq(m => m.WorkflowId, updatedWorkflow.WorkflowId);
            try
            {
                var workflowDto = _mapper.Map<WorkflowDto>(updatedWorkflow);
                if (session != null)
                    result = await _db.ReplaceOneAsync(session, filter, workflowDto);
                else result = await _db.ReplaceOneAsync(filter, workflowDto);
            }
            catch (AutoMapperMappingException e)
            {
                return new WorkflowActionResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
            return new WorkflowActionResponse("Workflow " + updatedWorkflow.WorkflowId, result.IsAcknowledged, "update success");
        }



        public async Task<bool> GetPendingWorkflowForListing(string listingId)
        {
            bool HasPendingWorkflows = false;
            try
            {
                var query = await
                    _db.AsQueryable()
                    .Where(w => w.Listing.ListingId == listingId
                            && (w.Status != WorkflowStatusEnum.Completed.ToString()
                            || w.Status != WorkflowStatusEnum.Rejected.ToString()))
                    .ToListAsync();
                if (query.Count > 0) HasPendingWorkflows = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return HasPendingWorkflows;
        }





    }
}