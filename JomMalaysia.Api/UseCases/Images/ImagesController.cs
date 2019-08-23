using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Api.UseCases.Images;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.ImageProcessingServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Images
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageProcessor _imageProcessor;
        private readonly ImagePresenter _presenter;

        public ImagesController(IImageProcessor imageProcessor, ImagePresenter presenter)
        {
            _presenter = presenter;
            _imageProcessor = imageProcessor;
        }

        // GET: api/Images/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> LoadAsync([FromRoute]string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            var result = await _imageProcessor.LoadImage(id, _presenter).ConfigureAwait(false);
            if (result.Success) return File(result.ImageByte, result.IMAGETYPE);

            return _presenter.ContentResult;
        }

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }
            var req = new RawImageRequest()
            {
                Image = image
            };
            await _imageProcessor.Handle(req, _presenter);
            return _presenter.ContentResult;
        }

    }
}
