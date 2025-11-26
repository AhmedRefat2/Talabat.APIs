using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;
using Talabat.Domain.Specifications.ProductSpecs;

namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        // GET By ID
        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes();
        }

        // GET ALL
        public ProductWithBrandAndCategorySpecifications(ProductSpecsParams specParams)
            : base( p =>
                specParams != null &&
                (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value) && 
                (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
            )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc": 
                        OrderBy = (p => p.Price); 
                        break;
                    case "priceDesc":
                        OrderByDesc = (p => p.Price);
                        break;
                    default:
                        OrderBy = (p => p.Name);
                        break;
                }
            }

            else OrderBy = (p => p.Name);

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
