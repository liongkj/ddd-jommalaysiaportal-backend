using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Services.ImageProcessingService;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<ImageProcessorResponse> SaveImageAsync(byte[] stream);
        
    }
}
