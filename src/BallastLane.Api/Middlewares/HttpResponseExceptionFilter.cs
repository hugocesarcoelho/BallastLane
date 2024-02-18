using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Findox.WebApi.Middlewares
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is Exception ex)
            {
                var objectResult = new Result<bool>(false);
                objectResult.AddError("unexpected_exception", ex.Message.ToString());

                context.Result = new JsonResult(objectResult)
                {
                    StatusCode = 500
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
