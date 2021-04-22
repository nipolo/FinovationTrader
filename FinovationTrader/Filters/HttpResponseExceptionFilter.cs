using Finovation.Core.Common.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FinovationTrader.API.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        private readonly ILogger _logger;

        public int Order => int.MaxValue - 100;

        public HttpResponseExceptionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is EntityAlreadyExistsException exception)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = exception.Status
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is AggregateNotFoundException aggregateNotFoundException)
            {
                context.Result = new ObjectResult(aggregateNotFoundException.Message)
                {
                    StatusCode = aggregateNotFoundException.Status
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                context.Result = new ObjectResult("Internal error.")
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }

            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, context.Exception.Message);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
