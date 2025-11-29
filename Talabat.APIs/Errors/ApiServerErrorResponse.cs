namespace Talabat.APIs.Errors
{
    public class ApiServerErrorResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiServerErrorResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

    }
}
