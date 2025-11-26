using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Product;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.ProductSpecs;
using Talabat.Domain.Specifications.ProductSpecs;

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
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery] ProductSpecsParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams);

            var products = await _productRepo.GetAllWithSpecsAsync(specs);

            var countSpecs = new ProductsWithFilterationForCountSpecification(specParams);

            var productsCountWithoutPagination = await _productRepo.GetCountAysnc(countSpecs);

            var productsPaginationResponse = new Pagination<Product>(
                specParams.PageIndex, specParams.PageSize, productsCountWithoutPagination, products);

            return Ok(productsPaginationResponse);
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
