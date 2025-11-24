using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;

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
        {
            var specs = new ProductWithBrandAndCategorySpecifications();

            var products = await _productRepo.GetAllWithSpecsAsync(specs);

            return Ok(products);
        }

        // GET: api/Products/5 [GET Product By Id]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {

            var specs = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepo.GetWithSpecsAsync(specs);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
    }
}
