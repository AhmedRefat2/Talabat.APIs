using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        // By Default Redirect verb : GET
        public ActionResult Error404(int code)
        {
            return code switch
            {
                404 => NotFound(new ApiResponse(404, "Resource Not Found")),
                401 => Unauthorized(new ApiResponse(401)),
                _ => StatusCode(code, new ApiResponse(code))
            };
        }
    }
}
