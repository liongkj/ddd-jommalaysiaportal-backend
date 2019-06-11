using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JomMalaysia.Core.Interfaces
{
    public interface IUseCaseHandlerAsync<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest :IUseCaseRequest<TUseCaseResponse>
    {
        Task<bool> Handle(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);

    }
}

//public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
//{
//    Task<bool> Handle(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
//}