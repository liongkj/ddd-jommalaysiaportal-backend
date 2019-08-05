using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetAllWorkflowUseCase : IGetAllWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;

        public GetAllWorkflowUseCase(IWorkflowRepository workflowRepository)
        {
            _workfowRepository = workflowRepository;
        }
        public bool Handle(GetAllWorkflowRequest message, IOutputPort<GetAllWorkflowResponse> outputPort)
        {
            var response = _workfowRepository.GetAllWorkflowByStatus(message.Status);
            //foreach(var c in response.Categories){
            //    foreach(var sub in message.Subcategories)
            //    c.Subcategories.Add(sub);
            //}
            if (!response.Success)
            {
                outputPort.Handle(new GetAllWorkflowResponse(response.Errors));
            }
            if (response.Workflows!=null)
                outputPort.Handle(new GetAllWorkflowResponse(response.Workflows, true));
            else
                outputPort.Handle(new GetAllWorkflowResponse(response.Errors, true));

            return response.Success;
            //throw new NotImplementedException();
            //TODO 

        }
    }
}
