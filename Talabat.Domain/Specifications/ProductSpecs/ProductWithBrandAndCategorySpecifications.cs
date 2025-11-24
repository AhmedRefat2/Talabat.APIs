using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

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
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId)
            : base( p => 
                (!brandId.HasValue || p.BrandId == brandId.Value) && 
                (!categoryId.HasValue || p.CategoryId == categoryId.Value)
            )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
        }

        private void AddIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
