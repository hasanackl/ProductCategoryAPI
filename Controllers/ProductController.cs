using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryAPI.Data;
using ProductCategoryAPI.Models;
using ProductCategoryAPI.Models.Dtos;

namespace ProductCategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [Route("api/[controller]")]
        [ApiController]
        [JwtAuthentication]
        public class ProductsController : ControllerBase
        {
            [HttpGet]
            public IActionResult GetProducts()
            {
                return Ok(new { Message = "Token is valid, products are listed." });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Categories = p.ProductCategories.Select(pc => pc.Category.Name).ToList()
            }).ToList();

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Categories = product.ProductCategories.Select(pc => pc.Category.Name).ToList()
            };

            return Ok(productDto);
        }


        [HttpPost]  // POST: api/product
        public async Task<ActionResult<Product>> CreateProduct(ProductCreateDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                ProductCategories = new List<ProductCategory>()
            };
            foreach (var categoryId in dto.CategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductUpdateDto dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;

            product.ProductCategories.Clear();

            foreach (var categoryId in dto.CategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]   // DELETE: api/product/{id}
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products
            .Include(p => p.ProductCategories)
            .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}