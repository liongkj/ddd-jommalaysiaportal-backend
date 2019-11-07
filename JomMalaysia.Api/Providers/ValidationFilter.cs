using System;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JomMalaysia.Api.Providers
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}