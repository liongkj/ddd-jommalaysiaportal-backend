using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Services.ImageProcessingServices;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<ImageProcessorResponse> SaveImageAsync(byte[] stream);
        
    }
}
