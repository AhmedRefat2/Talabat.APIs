namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400, "Validation Error you have made")
        {
            Errors = new List<string>();
        }
    }
}
