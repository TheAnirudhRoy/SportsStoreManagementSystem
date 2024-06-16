using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL.Models;

namespace SportsStoreManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInventoryController : ControllerBase
    {
        private readonly SportsDbContext _context;

        public ProductInventoryController(SportsDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductInventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInventory>>> GetProductInventoryDetails()
        {
            return await _context.ProductInventories
                                 .Include(pi => pi.Product)
                                 .Include(pi => pi.Sup)
                                 .ToListAsync();
        }

        // GET: api/ProductInventory/SupId/ProductId
        [HttpGet("{SupId}/{ProductId}")]
        public async Task<ActionResult<ProductInventory>> GetCurrentProductInventoryDetail(int SupId, int ProductId)
        {
            var productInventory = await _context.ProductInventories
                                                 .Include(pi => pi.Product)
                                                 .Include(pi => pi.Sup)
                                                 .FirstOrDefaultAsync(pi => pi.SupId == SupId && pi.ProductId == ProductId);

            if (productInventory == null)
            {
                return NotFound();
            }

            return productInventory;
        }

        // PUT: api/ProductInventory/SupId/ProductId
        [HttpPut]
        public async Task<IActionResult> PutProductInventory(ProductInventory productInventory)
        {
            _context.Entry(productInventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInventoryExists(productInventory.SupId, productInventory.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProductInventoryExists(int SupId, int ProductId)
        {
            return _context.ProductInventories.Any(e => e.SupId == SupId && e.ProductId == ProductId);
        }
    }
}



