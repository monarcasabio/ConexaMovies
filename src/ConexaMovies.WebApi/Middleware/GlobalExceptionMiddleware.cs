using ConexaMovies.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

public sealed class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (status, title) = ex switch
        {
            BadRequestException => (400, "Bad Request"),
            UnauthorizedException => (401, "Unauthorized"),
            NotFoundException => (404, "Not Found"),
            ConflictException => (409, "Conflict"),
            _ => (500, "An error occurred while processing your request.")
        };

        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = ex.Message,
            Instance = context.Request.Path
        });
    }
}
