using System.Collections.Generic;
using System.IO;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.ImageProcessingServices
{
    public class ImageLoadedResponse : UseCaseResponseMessage
    {
        public ImageLoadedResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ImageLoadedResponse(byte[] id, bool success = false, string message = null) : base(success, message)
        {
            ImageByte = id;
        }
        public byte[] ImageByte { get; }
        public string IMAGETYPE { get; } = "image/jpeg";


        public IEnumerable<string> Errors { get; }

    }
}
