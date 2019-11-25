using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.BLL.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FullStack.API.ErrorHandling
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
             
        public ApiExceptionFilter()
        {
           
        }


        public override void OnException(ExceptionContext context)
        {

            ApiError apiError = null;
            if (context.Exception is ApiException)
            {
                // handle explicit 'known' API errors
                var ex = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(ex.Message)
                {
                    Errors = ex.Errors
                };

                context.HttpContext.Response.StatusCode = ex.StatusCode;

               
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;
             
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg)
                {
                    Detail = stack
                };

                context.HttpContext.Response.StatusCode = 500;

               
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }
    }
}
