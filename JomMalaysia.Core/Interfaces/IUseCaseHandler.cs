using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Interfaces
{
    public interface IUseCaseHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        bool HandleAsync(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);

    }
}