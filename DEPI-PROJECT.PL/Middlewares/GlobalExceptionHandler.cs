using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;

namespace DEPI_PROJECT.PL.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message, details) = exception switch
            {
                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized access",
                    exception.Message
                ),
                
                ValidationException validationEx => (
                    StatusCodes.Status400BadRequest,
                    "Validation failed",
                    validationEx.Message
                ),
                
                BadRequestException badRequestEx => (
                    StatusCodes.Status400BadRequest,
                    "Bad request",
                    badRequestEx.Message
                ),
                
                ArgumentException or ArgumentNullException => (
                    StatusCodes.Status400BadRequest,
                    "Invalid argument",
                    exception.Message
                ),
                
                InvalidOperationException => (
                    StatusCodes.Status400BadRequest,
                    "Invalid operation",
                    exception.Message
                ),
                
                FormatException => (
                    StatusCodes.Status400BadRequest,
                    "Invalid format",
                    exception.Message
                ),
                
                NotFoundException => (
                    StatusCodes.Status404NotFound,
                    "Resource not found",
                    exception.Message
                ),
                
                TimeoutException => (
                    StatusCodes.Status408RequestTimeout,
                    "Request timeout",
                    "The request took too long to process"
                ),
                
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred",
                    exception.Message
                )
            };

            // Log the exception with appropriate level
            if (statusCode >= 500)
            {
                _logger.LogError(exception, "Server error occurred: {Message}", exception.Message);
            }
            else if (statusCode >= 400)
            {
                _logger.LogWarning(exception, "Client error occurred: {Message}", exception.Message);
            }

            var response = new ResponseDto<object>
            {
                Message = message,
                IsSuccess = false,
                Data = new { 
                        Exception = exception.GetType().Name,
                        Details = details,
                        StackTrace = exception.StackTrace 
                    }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, serializeOptions));
        }
    }
}
