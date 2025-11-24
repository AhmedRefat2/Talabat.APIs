using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Infrastructure._Data.Configurations.ProductConfigurations
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product.Property(P => P.Name).IsRequired().HasMaxLength(100);
            product.Property(P => P.Description).IsRequired();
            product.Property(P => P.PictureUrl).IsRequired();

            product.Property(P => P.Price).HasColumnType("decimal(18,2)");

            product.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);

            product.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);
        }
    }
}
