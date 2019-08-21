using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageProcessor _imageProcessor;

        public ImagesController(IImageProcessor imageProcessor)
        {
            _imageProcessor = imageProcessor;
        }

        // GET: api/Images/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Images/{type}
        [HttpPost]
        public async Task<int> UploadImage([FromForm] IFormFile image)
        {
            await _imageProcessor.ProcessImage(image);
            return 1;
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
