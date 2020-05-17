using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreApi.WebApi
{
    public class ExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = 500;
            response.ContentType = "application/json";
            context.Result = new ObjectResult("something went wrong we are working on it...");
        }
    }
}

