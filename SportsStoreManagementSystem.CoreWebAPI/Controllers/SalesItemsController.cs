using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.DAL.Models;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemsController : ControllerBase
    {
        private readonly SportsDbContext _context;

        public SalesItemsController(SportsDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesItem/{salesId}/{productId}
        [HttpGet("{salesId}/{productId}")]
        public async Task<ActionResult<SalesItem>> GetSalesItemBySalesIdAndProductId(int salesId, int productId)
        {
            var salesItem = await _context.SalesItems
                                          .FirstOrDefaultAsync(si => si.SalesId == salesId && si.ProductId == productId);

            if (salesItem == null)
            {
                return NotFound();
            }

            return salesItem;
        }

        // GET: api/SalesItem/SalesId/{salesId}
        [HttpGet("SalesId/{salesId}")]
        public async Task<ActionResult<IEnumerable<SalesItem>>> GetSalesItemBySalesId(int salesId)
        {
            var salesItems = await _context.SalesItems
                                           .Include(si => si.Product)
                                           .Where(si => si.SalesId == salesId)
                                           .ToListAsync();

            if (salesItems == null || !salesItems.Any())
            {
                return NotFound();
            }

            return salesItems;
        }

        // POST: api/SalesItem
        //[HttpPost]
        //public async Task<ActionResult<SalesItem>> PostSalesItem(SalesItem salesItem)
        //{
        //    _context.SalesItems.Add(salesItem);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSalesItemBySalesIdAndProductId", new 
        //    { salesId = salesItem.SalesId, productId = salesItem.ProductId }, salesItem);
        //}

        [HttpPost("Return")]
        public async Task<IActionResult> ReturnSalesItems([FromBody] List<ReturnItemDto> returnItemsDto)
        {
            if (returnItemsDto == null || !returnItemsDto.Any())
            {
                return BadRequest("return items list is empty.");
            }

            foreach (var item in returnItemsDto)
            {
                SalesItem salesItem = _context.SalesItems.Where(si => si.SalesId == item.SalesId && si.ProductId == item.ProductId).FirstOrDefault();
                ProductInventory productInventory = _context.ProductInventories.Where(pi => pi.ProductId == item.ProductId && pi.SupId == item.SupId).FirstOrDefault();

                salesItem.Qty -= item.Qty;
                productInventory.Stocks += item.Qty;
            }

            _context.SaveChanges();
            return Ok(returnItemsDto[0].SalesId);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalesItems([FromBody] List<SalesItemDto> salesItemsDto)
        {
            if (salesItemsDto == null || !salesItemsDto.Any())
            {
                return BadRequest("Sales items list is empty.");
            }

            SalesHistory newOrder = new SalesHistory
            {
                CustomerId = salesItemsDto[0].CustomerId,
                PurchaseDate = DateTime.Now,
                BillTotal = 0
            };

            await _context.SalesHistories.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in salesItemsDto)
            {
                newOrder.BillTotal += item.PricePerItem * item.Qty;
                var orderDetail = new SalesItem
                {
                    CustomerId = Convert.ToInt32(newOrder.CustomerId),
                    SalesId = newOrder.SalesId,
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    PricePerItem = item.PricePerItem,
                    SupId = item.SupId
                };
                await _context.AddAsync(orderDetail);
                ProductInventory inventory = await _context.ProductInventories.Where(pi => pi.ProductId == item.ProductId && pi.SupId == item.SupId).FirstOrDefaultAsync();
                if (inventory != null)
                {
                    inventory.Stocks = inventory.Stocks - item.Qty;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(newOrder.SalesId);
        }
    }
}
