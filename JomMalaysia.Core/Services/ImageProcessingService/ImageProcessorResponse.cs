using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.ImageProcessingService
{
    public class ImageProcessorResponse : UseCaseResponseMessage
    {
        public ImageProcessorResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ImageProcessorResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            ImageId = id;
        }
        public string ImageId { get; }
        public IEnumerable<string> Errors { get; }

    }
}
