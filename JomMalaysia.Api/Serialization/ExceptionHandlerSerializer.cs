using System;
using System.Net;
using System.Threading.Tasks;
using JomMalaysia.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JomMalaysia.Api.Serialization
{
    public class ExceptionHandlerSerializer
    {

        private readonly RequestDelegate next;
        public ExceptionHandlerSerializer(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            // if (ex is ValidationException) code = HttpStatusCode.BadRequest;
            // else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (ex is MyException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
