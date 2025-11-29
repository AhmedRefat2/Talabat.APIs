using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;
namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Take An Action With The Request

                await _next.Invoke(httpContext); // Go to Next Middle ware

                // Take An Action With The Response 
            }
            catch (Exception ex)
            {
                // 1. Log Exception At Console While Development
                _logger.LogError(ex.Message);

                // 2. Production 
                // Log At Database Or Files 
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ? new ApiServerErrorResponse(500, ex.Message,
                    ex.StackTrace) : new ApiServerErrorResponse(500);

                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
                
            }
        }
    }
}
