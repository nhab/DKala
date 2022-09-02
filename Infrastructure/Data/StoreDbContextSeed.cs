using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                using (context)
                {
                    if (!context.Products.Any())
                    {
                        var brandsData="[{\"Name\":\"Nike\"}]";
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        context.ProductBrands.AddRange(brands);

                        var typesData= "[{\"Name\":\"Shoe\"}]";
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        context.ProductTypes.AddRange(types);
                        await context.SaveChangesAsync();

                        var brandid = context.ProductBrands.Single<ProductBrand>().Id;
                        var typeid=context.ProductTypes.Single<ProductType>().Id;

                        var productsData =
                          File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        
                        context.Products.AddRange(products);
                        //List<Product> t = context.Products.ToList<Product>();
                        await context.SaveChangesAsync();
                    }
                }
        // repeat the proccess for ProductBrands and ProductTypes
      }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
