using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JomMalaysia.Core.Interfaces
{
    public interface IImageProcessor
    {
        Task<string> ProcessImage(IFormFile File);
    }
}
