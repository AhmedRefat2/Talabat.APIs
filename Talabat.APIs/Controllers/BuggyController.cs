using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Controllers;
using Talabat.Core.Entities;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 1. Not Found Error
        [HttpGet("notFound")] // GET : api/buggy/notFound
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(100);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        // 2. Server Error
        [HttpGet("serverError")] // GET : api/buggy/serverError
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var ProductToReturn = product.ToString(); // Will Throw Exception : Server error
            return Ok(ProductToReturn);
        }

        // 3. Bad Request
        [HttpGet("badRequest")] // GET : api/buggy/badRequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        // 4. Validation Error
        [HttpGet("badRequest/{id}")] // GET : api/buggy/badRequest/five 
        public ActionResult GetBadRequest(int id) // Validation Error => Bad Request Type
        {
            return Ok();
        }

        // 5. UnAuthorized
        [HttpGet("unAuthorized")]
        public ActionResult GetUnAuthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
