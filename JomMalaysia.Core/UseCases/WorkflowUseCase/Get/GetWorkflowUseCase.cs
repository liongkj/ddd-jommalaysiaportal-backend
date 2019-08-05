using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetWorkflowUseCase : IGetWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;
        private readonly IListingRepository _listingRepository;

        public GetWorkflowUseCase(IWorkflowRepository workflowRepository, IListingRepository listingRepository)
        {
            _workfowRepository = workflowRepository;
            _listingRepository = listingRepository;
        }
        public bool Handle(GetWorkflowRequest message, IOutputPort<GetWorkflowResponse> outputPort)
        {
            var workflowResponse = _workfowRepository.GetWorkflowById(message.WorkflowId);

            //foreach(var c in response.Categories){
            //    foreach(var sub in message.Subcategories)
            //    c.Subcategories.Add(sub);
            //}

            outputPort.Handle(workflowResponse);


            return workflowResponse.Success;
            //throw new NotImplementedException();
            //TODO 

        }
    }
}
