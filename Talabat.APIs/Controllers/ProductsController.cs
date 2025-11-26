using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoriesRepo;

        public ProductsController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoriesRepo)
        {
            _productRepo = productRepo;
            _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
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

        // GET: api/Products/brands [GET All Brands]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
            => Ok(await _brandsRepo.GetAllAsync());

        // GET: api/Products/categories [GET All Categories]
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
            => Ok(await _categoriesRepo.GetAllAsync());



    }
}
