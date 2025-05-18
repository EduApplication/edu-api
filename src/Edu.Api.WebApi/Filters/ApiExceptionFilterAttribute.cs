using Edu.Api.Domain.Exceptions;
using Edu.Api.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Edu.Api.WebApi.Filters;

/// <summary>
/// Filter for handling exceptions in API controllers
/// </summary>
public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    private readonly ILogger<ApiExceptionFilterAttribute> _logger;
    private readonly IWebHostEnvironment _environment;

    /// <summary>
    /// Initializes a new instance of the API exception filter
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="environment">The web host environment</param>
    public ApiExceptionFilterAttribute(
        ILogger<ApiExceptionFilterAttribute> logger,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _environment = environment;
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(EntityNotFoundException), HandleEntityNotFoundException },
            { typeof(ValidationException), HandleValidationException },
            { typeof(AccessDeniedException), HandleAccessDeniedException },
            { typeof(BusinessRuleException), HandleBusinessRuleException },
            { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
        };
    }

    /// <summary>
    /// Called when an exception occurs
    /// </summary>
    /// <param name="context">The exception context</param>
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private void HandleEntityNotFoundException(ExceptionContext context)
    {
        var exception = (EntityNotFoundException)context.Exception;

        var details = new ErrorResponse
        {
            Code = exception.Code,
            Message = exception.Message,
            Details = exception.Details,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ErrorResponse
        {
            Code = exception.Code,
            Message = exception.Message,
            Details = exception.Details,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleAccessDeniedException(ExceptionContext context)
    {
        var exception = (AccessDeniedException)context.Exception;

        var details = new ErrorResponse
        {
            Code = exception.Code,
            Message = exception.Message,
            Details = exception.Details,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
        context.ExceptionHandled = true;
    }

    private void HandleBusinessRuleException(ExceptionContext context)
    {
        var exception = (BusinessRuleException)context.Exception;

        var details = new ErrorResponse
        {
            Code = exception.Code,
            Message = exception.Message,
            Details = exception.Details,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleUnauthorizedAccessException(ExceptionContext context)
    {
        var details = new ErrorResponse
        {
            Code = "access_denied",
            Message = "Access denied",
            Path = context.HttpContext.Request.Path
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
        context.ExceptionHandled = true;
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
            );

        var details = new ErrorResponse
        {
            Code = "validation_failed",
            Message = "One or more validation errors occurred",
            Details = errors,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An unhandled exception occurred");

        var details = new ErrorResponse
        {
            Code = "internal_server_error",
            Message = _environment.IsDevelopment()
                ? context.Exception.Message
                : "An unexpected error occurred",
            Details = _environment.IsDevelopment()
                ? new
                {
                    StackTrace = context.Exception.StackTrace,
                    Source = context.Exception.Source,
                    InnerException = context.Exception.InnerException?.Message
                }
                : null,
            Path = context.HttpContext.Request.Path
        };

        context.Result = new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
        context.ExceptionHandled = true;
    }
}