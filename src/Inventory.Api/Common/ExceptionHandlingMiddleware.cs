using FluentValidation;
using Inventory.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;
using Inventory.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Inventory.Api.Common.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            _logger.LogWarning(exception, "Validation failed");

            context.Response.ContentType = "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.BadRequest;

            var response = new
            {
                Message = "Validation failed",
                Errors = exception.Errors.Select(error =>
                    error.ErrorMessage)
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (BusinessException exception)
        {
            _logger.LogWarning(exception, "Business exception occurred");
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    Message = exception.Message
                });
        }
        catch (DomainException exception)
        {
            _logger.LogWarning(exception, "Domain exception occurred");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    Message = exception.Message
                });
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unexpected error occurred");
            context.Response.ContentType = "application/json";

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                Message = "Internal server error"
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}