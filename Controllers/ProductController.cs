using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;
using StockAPI.StructureJSON;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;   
        }

        // [HttpPost]
        // public async Task<ActionResult<List<Product>>> AddProduct(Product product){
        //     _context.Products.Add(product);
        //     await _context.SaveChangesAsync();

        //     return Ok(await _context.Products.ToListAsync());
        // }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductInputModel ProductInput)
        {
            try
            {
                Product product = new Product
                {
                    num_produit = ProductInput.num_produit,
                    design = ProductInput.design,
                    description = ProductInput.description,
                    image = ProductInput.image
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(){
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProductById(string id){
            var product = await _context.Products.FindAsync(id);
            if(product == null){
                return BadRequest("Product not found.");
            }
            return Ok(product);
        } 

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Product>>> PutAsync(string id, ProductUpdateModel product)
        {
            var exist_product = await _context.Products.FindAsync(id);

            if (exist_product == null)
            {
                return NotFound();
            }

            exist_product.description = product.description;
            exist_product.design = product.design;
            exist_product.image = product.image;


            _context.Products.Update(exist_product);
            await _context.SaveChangesAsync();

            return Ok(exist_product);
        }

        [HttpGet("low-stock")]
        public IActionResult GetProductsLowStock()
        {
            try
            {
                var query = "SELECT * FROM Products WHERE quantite < 10";
                var products = _context.Products.FromSqlRaw(query).ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProductById(string id){
            var product = await _context.Products.FindAsync(id);
            if(product == null){
                return BadRequest("Product whith number: "+id+" doesn't exist.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Product removed successfully");    
        }
    }
}