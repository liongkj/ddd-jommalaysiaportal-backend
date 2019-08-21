using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace JomMalaysia.Core.Services.ImageProcessingService
{
    public class ImageProcessor : IImageProcessor
    {
        private readonly IImageRepository _repo;
        public ImageProcessor(IImageRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> ProcessImage(IFormFile File)
        {
            ImageProcessorResponse response;
            // full path to file in temp location

            var fileName = Path.GetFileName(File.FileName);
            using (var ms = new MemoryStream())
            {
                File.CopyTo(ms);
                var fileBytes = ms.ToArray();

                response = await _repo.SaveImageAsync(fileBytes);
                // act on the Base64 data
            }

            //save to database

            //return preview image
            return response.ImageId;
        }
    }
}
