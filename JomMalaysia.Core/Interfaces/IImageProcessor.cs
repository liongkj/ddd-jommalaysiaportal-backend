using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Services.ImageProcessingServices;
using Microsoft.AspNetCore.Http;

namespace JomMalaysia.Core.Interfaces
{
    public interface IImageProcessor : IUseCaseHandlerAsync<RawImageRequest, ImageProcessorResponse>
    {
        Task<ImageLoadedResponse> LoadImage(string imageId, IOutputPort<ImageLoadedResponse> outputPort);
    }
}
