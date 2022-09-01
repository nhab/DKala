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
        public static async Task SeedAsync(StoreDbContext context /*, ILoggerFactory loggerFactory*/)
        {
            try
            {
                if (!context.Products. Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
               
        // repeat the proccess for Products and Produdt Types
      }
            catch (Exception ex)
            {
                // var logger = loggerFactory.CreateLogger<StoreDbContextSeed>();
                //  logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

            }
        }

    }
}
