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
        public CreateWorkflowResponse CreateWorkflow(List<Workflow> workflows)
        {
            var WorkflowDtos = _mapper.Map<List<WorkflowDto>>(workflows);

            try
            {
                _db.InsertMany(WorkflowDtos);
            }
            catch (Exception e)
            {
                return new CreateWorkflowResponse(new List<string> { e.Message, "Workflow repository error" }, false);
            }
            return new CreateWorkflowResponse(workflows.Count + " inserted", true);
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
    }
}
