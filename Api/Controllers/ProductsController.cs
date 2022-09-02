using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repository)
        {
            _repo = repository;
        }

        //[HttpGet("seed")]

        //public   void Seed()
        //{
        //      _repo.Seed();
        //}
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            try
            {
                var products = await _repo.GetProductsAsync();
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
            return await _repo.getProductByIdAsync(Id);
        }
    }

}