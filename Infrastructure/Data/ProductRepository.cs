﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository  //and Implement it
    {
        private readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id==id );
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public void Seed()
        {
            //await  _context.SeedAsync();

            using ( _context)
            {
                try
                {
                    if (!_context.Products.Any())
                    {
                        //var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                        var productsData = "[{\"Name\":\"cvxc\" ,\"ProductBrandId\":1 ,\"ProductTypeId\":2 },{\"Name\":\"dfgfc\",\"ProductBrandId\":1,\"ProductTypeId\":2 }]";
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        foreach (var item in products)
                        {
                            Product p = new Product();
                            p.Name = item.Name;
                            _context.Products.Add(p);
                        }
                        var y = _context.Products.ToList<Product>();
                        _context.SaveChangesAsync();
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
}
