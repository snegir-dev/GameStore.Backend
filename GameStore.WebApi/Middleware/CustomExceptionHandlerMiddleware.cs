using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace GameStore.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (ex)
        {
            
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