using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsController(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }


        // GET: api/Products [GET All Products]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
            => Ok(await _productRepo.GetAllAsync());

        // GET: api/Products/5 [GET Product By Id]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepo.GetAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
