using System.Net;
using System.Text.Json;
using FluentValidation;
using GameStore.Application.Common.Exceptions;

namespace GameStore.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, 
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error - {E}", e);
            await HandlerExceptionAsync(context, e);
        }
    }

    private Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (ex)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case RecordCreateException createException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(createException.Errors);
                break;
            case NotFoundException foundException:
                statusCode = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(foundException.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = ex.Message });
        }

        return context.Response.WriteAsync(result);
    }
}