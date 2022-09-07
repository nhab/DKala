using Core.Entities;
using Core.Interfaces;
using Core.specifications;
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
        private  IGenericRepository<Product> _productsRepo;
        private IGenericRepository<ProductBrand> _productBrandRepo;
        private IGenericRepository<ProductType> _ProductTyeRepo;

        public ProductController(IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTyeRepo)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _ProductTyeRepo = productTyeRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("typs")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            return Ok(await _ProductTyeRepo.ListAllAsync());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(Id);

            return await _productsRepo.GetEntityWithSpec(spec);
        }
    }

}