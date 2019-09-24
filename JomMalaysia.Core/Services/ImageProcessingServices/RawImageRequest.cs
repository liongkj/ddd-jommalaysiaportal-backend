using JomMalaysia.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JomMalaysia.Core.Services.ImageProcessingServices
{
    public class RawImageRequest : IUseCaseRequest<ImageProcessorResponse>
    {
        public IFormFile Image { get; set; }

    }
}
