using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.ImageProcessingServices
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
        public string ImageUrl
        {
            get
            {
                return $"{ Framework.Constant.APIConstant.API.Path.Image}/{ImageId}";
            }


        }
        public IEnumerable<string> Errors { get; }

    }
}
