using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
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
        public CreateWorkflowResponse CreateWorkflow(Workflow workflow, IClientSessionHandle session)
        {
            var WorkflowDto = _mapper.Map<WorkflowDto>(workflow);

            try
            {
                _db.InsertOne(WorkflowDto);
                
            }
            catch (Exception e)
            {
                return new CreateWorkflowResponse(new List<string> { e.Message, "Error saving workflow" }, false);
            }
            return new CreateWorkflowResponse(WorkflowDto.Id + " inserted", true);
        }



        public GetAllWorkflowResponse FindByListing(List<string> listingIds, WorkflowStatusEnum workflowStatus)
        {
            var query =
                   _db.AsQueryable()
               .Where(W => W.Status.Equals(workflowStatus.ToString()))

               .OrderBy(c => c.Created)
               .ToList();

            List<Workflow> Workflows = _mapper.Map<List<Workflow>>(query);
            var response = Workflows.Count < 1 ?
                new GetAllWorkflowResponse(new List<string> { "No workflow found" }, false) :
                new GetAllWorkflowResponse(Workflows, true);
            return response;
        }

        public GetWorkflowResponse GetWorkflowById(string workflowId)
        {
            var response = new GetWorkflowResponse(new List<string> { });
            try
            {
                var query =
                    _db.AsQueryable()
                    .Where(W => W.Id == workflowId);
                var workflow = _mapper.Map<Workflow>(query);
                return new GetWorkflowResponse(workflow, true);
            }
            catch(Exception e)
            {
                var Errors = new List<string>();
                Errors.Add(e.ToString());
                return new GetWorkflowResponse(Errors);
            }
            
        }

        public GetAllWorkflowResponse GetAllWorkflowByStatus(WorkflowStatusEnum status, int counterpage = 10, int page = 0)
        {
            //todo add paging
            List<WorkflowDto> query = new List<WorkflowDto>();
            if (!status.Equals(WorkflowStatusEnum.All))
            {
                query =
                    _db.AsQueryable()
                    .Where(W => W.Status.Equals(status.ToString()))
                    .OrderBy(c => c.Created)
                    .ToList();
            }
            else
            {
                query =
                    _db.AsQueryable()
                    .OrderBy(c => c.Created)
                    .ToList();
            }
            List<Workflow> Workflows = _mapper.Map<List<WorkflowDto>,List<Workflow>>(query);
            var response = Workflows.Count < 1 ?
                new GetAllWorkflowResponse(new List<string> { "No workflow found" }, false) :
                new GetAllWorkflowResponse(Workflows, true);
            return response;
        }
    }
}
