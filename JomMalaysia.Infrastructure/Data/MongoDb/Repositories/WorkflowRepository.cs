using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase;
using JomMalaysia.Core.UseCases.WorkflowUseCase;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        public async Task<ListingWorkflowResponse> CreateWorkflowAsyncWithSession(Workflow workflow, IClientSessionHandle session)
        {
            var WorkflowDto = _mapper.Map<WorkflowDto>(workflow);

            try
            {
                await _db.InsertOneAsync(session, WorkflowDto);

            }
            catch (Exception e)
            {
                return new ListingWorkflowResponse(new List<string> { e.Message, "Error saving workflow" }, false);
            }
            return new ListingWorkflowResponse(WorkflowDto.Id + " created", true);
        }

        public async Task<GetWorkflowResponse> GetWorkflowByIdAsync(string workflowId)
        {
            Workflow workflow;
            try
            {
                var query = await
                    _db.AsQueryable()
                    .Where(W => W.Id == workflowId)
                    .FirstOrDefaultAsync();
                workflow = _mapper.Map<Workflow>(query);
                var temp = Converted(query.Listing);
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
            List<Workflow> Workflows;
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
                Workflows = _mapper.Map<List<Workflow>>(query);
                int i = 0;
                foreach (WorkflowDto workflow in query)
                {

                    var temp = Converted(workflow.Listing);
                    if (temp != null)
                    {
                        Workflows[i].Listing = temp;
                    }
                    i++;
                }

            }
            catch (AutoMapperMappingException e)
            {
                return new GetAllWorkflowResponse(new List<string> { "Mapping error" }, false, e.ToString());
            }
            catch (Exception e)
            {
                return new GetAllWorkflowResponse(new List<string> { "Unknown error" }, false, e.ToString());
            }
            var response = Workflows.Count < 1 ?
                new GetAllWorkflowResponse(new List<string> { "No workflow found" }, false) :
                new GetAllWorkflowResponse(Workflows, true);
            return response;
        }


        public async Task<WorkflowActionResponse> UpdateAsync(Workflow updatedWorkflow, IClientSessionHandle session = null)
        {
            ReplaceOneResult result;

            FilterDefinition<WorkflowDto> filter = Builders<WorkflowDto>.Filter.Eq(m => m.Id, updatedWorkflow.WorkflowId);
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
                return new WorkflowActionResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            return new WorkflowActionResponse("Workflow " + updatedWorkflow.WorkflowId, result.IsAcknowledged, "update success");
        }

        #region private helper method
        private Listing Converted(ListingSummaryDto list)
        {
            if (list != null)
            {
                if (GetListingTypeHelper(list).Equals(typeof(EventListing)))
                {
                    var i = _mapper.Map<EventListing>(list);

                    return i;
                }

                if (GetListingTypeHelper(list).Equals(typeof(PrivateListing)))
                {
                    var i = _mapper.Map<PrivateListing>(list);
                    return i;
                }
            }
            return null;
        }

        private Type GetListingTypeHelper(ListingSummaryDto list)
        {
            if (list.ListingType == ListingTypeEnum.Event.ToString())
            {
                return typeof(EventListing);

            }
            if (list.ListingType == ListingTypeEnum.Private.ToString())
            {
                return typeof(PrivateListing);
            }
            throw new ArgumentException("Error taking listing info from database ");
            #endregion

        }



    }
}