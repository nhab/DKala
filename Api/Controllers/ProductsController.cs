using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly StoreDbContext _context;
        public ProductController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("seed")]

        public async void Seed()
        {
            await StoreDbContextSeed.SeedAsync(_context);
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message as string);
            }
        }

        //https://localhost:5001/api/Product/2
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }
    }

}