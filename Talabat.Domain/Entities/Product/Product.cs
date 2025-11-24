using System.ComponentModel.DataAnnotations.Schema;

namespace Talabat.Core.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; } = null!;

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; } = null!;

    }
}
