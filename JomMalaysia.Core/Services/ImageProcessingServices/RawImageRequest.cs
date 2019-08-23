using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JomMalaysia.Core.Services.ImageProcessingServices
{
    public class RawImageRequest : IUseCaseRequest<ImageProcessorResponse>
    {
        public IFormFile Image { get; set; }

    }
}
