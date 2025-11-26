using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Specifications.ProductSpecs
{
    public class ProductSpecsParams
    {
        private int pageSize = 5;
        private const int maxPageSize = 10;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

        private string? search;
        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

    }
}
