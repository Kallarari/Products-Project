using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using YourAppNamespace;
using dotnet_API.Entities;
using ProductsNamespace;

namespace ProductsNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext _context;

        public ProductsController(ProductsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Where(p => !p.IsDeleted && p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SupplierId = p.SupplierId,
                    CategoryId = p.CategoryId
                })
                .FirstOrDefaultAsync();

            if (product == null)
                return NotFound(new { Message = "Product not found or deleted" });

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest(new { Message = "ID mismatch" });

            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted)
                return NotFound(new { Message = "Product not found or deleted" });

            product.Name = updatedProduct.Name;
            product.SupplierId = updatedProduct.SupplierId;
            product.CategoryId = updatedProduct.CategoryId;
            product.IsDeleted = updatedProduct.IsDeleted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { Message = "Failed to update product" });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || product.IsDeleted)
                return NotFound(new { Message = "Product not found or already deleted" });

            product.IsDeleted = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new { Message = "Failed to delete product" });
            }

            return NoContent();
        }
    }
}
