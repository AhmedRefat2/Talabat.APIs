using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
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
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoriesRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, 
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoriesRepo, 
            IMapper mapper)
        {
            _productRepo = productRepo;
            _brandsRepo = brandsRepo;
            _categoriesRepo = categoriesRepo;
            _mapper = mapper;
        }


        // GET: api/Products [GET All Products]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecsParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(specParams);

            var products = await _productRepo.GetAllWithSpecsAsync(specs);

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

            var countSpecs = new ProductsWithFilterationForCountSpecification(specParams);

            var productsCountWithoutPagination = await _productRepo.GetCountAysnc(countSpecs);

            var productsPaginationResponse = new Pagination<ProductToReturnDto>(
                specParams.PageIndex, specParams.PageSize, productsCountWithoutPagination, mappedProducts);

            return Ok(productsPaginationResponse);
        }

        // GET: api/Products/5 [GET Product By Id]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {

            var specs = new ProductWithBrandAndCategorySpecifications(id);

            var product = await _productRepo.GetWithSpecsAsync(specs);

            if (product == null)
                return NotFound();

            var mappedProduct = _mapper.Map<ProductToReturnDto>(product);

            return Ok(mappedProduct);
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
