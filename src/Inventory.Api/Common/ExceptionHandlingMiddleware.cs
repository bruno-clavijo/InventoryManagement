using FluentValidation;
using Inventory.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Inventory.Api.Common.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
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
            context.Response.StatusCode = 400;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    Message = exception.Message
                });
        }
        catch (Exception)
        {
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