using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MRA.AssetsManagement.Application.Common.Exceptions;

namespace MRA.AssetsManagement.Web.Server.Filters
{
    public class ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger) : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = context switch
            {
                { Exception: ValidationException } => HandleValidationException(context),
                { Exception: UnauthorizedAccessException } => HandleUnauthorizedAccessException(context),
                { Exception: NotFoundEntityException } => HandleNotFoundException(context),
                { ModelState: { IsValid: false } } => HandleInvalidModelStateException(context),
                _ => HandleUnknownException(context)
            };

            base.OnException(context);
        }

        public bool HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException)context.Exception;
            ProblemDetails details = new ValidationProblemDetails(exception.Errors)
            {
                Title = "Bad Request.",
                Detail = "One or more validation errors was occured",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new BadRequestObjectResult(details);

            return true;
        }


        public bool HandleUnknownException(ExceptionContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = context.Exception.Message,
            };

            context.Result = new ObjectResult(details);

            return true;
        }


        private bool HandleInvalidModelStateException(ExceptionContext context)
        {
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            return true;
        }
        
        private bool HandleUnauthorizedAccessException(ExceptionContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                Detail = "unauthorized"
            };

            context.Result = new ObjectResult(details) { StatusCode = StatusCodes.Status400BadRequest };

            return true;
        }

        public bool HandleNotFoundException(ExceptionContext context)
        {
            ProblemDetails details = new()
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Not Found",
                Detail = context.Exception.Message,
            };
            
            context.Result = new NotFoundObjectResult(details);

            return true;
        }
    }
}
