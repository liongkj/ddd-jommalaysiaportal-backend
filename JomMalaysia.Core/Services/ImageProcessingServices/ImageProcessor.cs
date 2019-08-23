using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace JomMalaysia.Core.Services.ImageProcessingServices
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly IImageRepository _repo;
        //private readonly ImageOptions _options;
        public ImageProcessor(IImageRepository repo)
        {
            _repo = repo;
            //_options = options;
        }
        #region process image
        public async Task<bool> Handle(RawImageRequest message, IOutputPort<ImageProcessorResponse> outputPort)
        {
            var img = message.Image;
            if (!string.IsNullOrEmpty(img.ContentType) && img.ContentType.ToLower().StartsWith("image"))
            {

                string FileExtension = Path.GetExtension(img.FileName).ToLower();
                //if (_options.FileTypes.Split(',').Contains(FileEextension))
                //{
                var outputstream = new MemoryStream();
                using (var inputstream = img.OpenReadStream())
                using (var image = Image.Load(inputstream, out IImageFormat format))
                {
                    //logo
                    var clone = image.Clone(i => i.Resize(100, 100));
                    clone.SaveAsJpeg(outputstream);
                }
                var res = await _repo.SaveImageAsync(outputstream.ToArray()).ConfigureAwait(false);
                outputPort.Handle(new ImageProcessorResponse(res.ImageId, res.Success));
                return res.Success;
                //}
            }
            outputPort.Handle(new ImageProcessorResponse("image process fail"));
            return false;
        }
        #endregion

        #region load image
        public async Task<ImageLoadedResponse> LoadImage(string imageId, IOutputPort<ImageLoadedResponse> outputPort)
        {
            ImageLoadedResponse response;
            if (string.IsNullOrEmpty(imageId))
            {
                throw new ArgumentException("message", nameof(imageId));
            }
            try
            {
                response = await _repo.LoadImageAsync(imageId);
                // outputPort.Handle(response);
                // return response;

            }
            catch (Exception e)
            {
                response = new ImageLoadedResponse(new List<string> { e.ToString() }, false, e.Message);
            }
            outputPort.Handle(response);
            return response;
        }
        #endregion
    }
}
