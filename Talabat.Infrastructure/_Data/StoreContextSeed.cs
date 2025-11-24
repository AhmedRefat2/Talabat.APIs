using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext context)
        {

            if (context.ProductBrands.Count() == 0)
            {
                // Seed From Json File
                var brandsData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {

                    //brands = brands.Select(b => new ProductBrand() // To Solve Identity Problem 
                    //{
                    //    Name = b.Name
                    //}).ToList(); 

                    context.ProductBrands.AddRange(brands);
                    await context.SaveChangesAsync();
                }
            }

            if (context.ProductCategories.Count() == 0)
            {
                var categoriesData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories?.Count() > 0)
                {
                    context.ProductCategories.AddRange(categories);
                    await context.SaveChangesAsync();
                }
            }

            if (context.Products.Count() == 0)
            {
                var productsData = File.ReadAllText("../Talabat.Infrastructure/_Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count() > 0)
                {   
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}