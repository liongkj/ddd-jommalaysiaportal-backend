using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DnsClient;
using JomMalaysia.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JomMalaysia.Api.Providers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)

        {

            if (context.Exception is NotAuthorizedException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new JsonResult(
                    new ExceptionResponse(((NotAuthorizedException)context.Exception).Message));

                return;
            }

            if (context.Exception is BadRequestException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    new ExceptionResponse(((BadRequestException)context.Exception).Message));

                return;
            }



            if (context.Exception is DuplicatedException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Result = new JsonResult(
                    new ExceptionResponse(((DuplicatedException)context.Exception).Message));
                return;
            }

            if (context.Exception is DnsResponseException)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                context.Result = new JsonResult(
                    new ExceptionResponse(((DnsResponseException)context.Exception).Message));

                return;
            }


            var code = HttpStatusCode.InternalServerError;


            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace

            });
        }
    }
}
