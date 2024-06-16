using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.BL;
using Microsoft.Extensions.FileProviders;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly ProductsBL productsObj = new ProductsBL();
        //private readonly SportsDbContext _context;

        //public ProductsController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(productsObj.GetProductsBL());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = productsObj.GetProductBL(id);

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            bool productAdded = productsObj.PostProductBL(product);
            if(productAdded)
            {
                return Ok();
            }
            return BadRequest();

            //return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            SportsStoreEnum sportsStoreEnum = productsObj.EditProductsBL(id, product);
            
            if (sportsStoreEnum == SportsStoreEnum.BadRequest)
            {
                return BadRequest();
            }
            if (sportsStoreEnum == SportsStoreEnum.NotFound)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryInInventory(int categoryId)
        {
            var products = productsObj.GetProductsByCategoryInInventoryBL(categoryId);

            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("bySupplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySupplier(int supplierId)
        {
            var products = productsObj.GetProductsBySupplierBL(supplierId);

            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}
