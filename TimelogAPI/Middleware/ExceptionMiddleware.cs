using Microsoft.AspNetCore.Mvc;
using TimelogAPI.Middleware.Exceptions;

namespace TimelogAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                var (statusCode, title) = ex switch
                {
                    NotFoundException => (StatusCodes.Status404NotFound, "Not Found"),
                    ValidationException => (StatusCodes.Status400BadRequest, "Validation Error"),
                    _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
                };

                var problem = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = ex.Message
                };

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = statusCode;
                var json = System.Text.Json.JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
