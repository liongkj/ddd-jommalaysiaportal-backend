using System.Threading.Tasks;
using JomMalaysia.Core.Services.ImageProcessingServices;


namespace JomMalaysia.Core.Interfaces
{
    public interface IImageProcessor : IUseCaseHandlerAsync<RawImageRequest, ImageProcessorResponse>
    {
        Task<ImageLoadedResponse> LoadImage(string imageId, IOutputPort<ImageLoadedResponse> outputPort);
    }
}
