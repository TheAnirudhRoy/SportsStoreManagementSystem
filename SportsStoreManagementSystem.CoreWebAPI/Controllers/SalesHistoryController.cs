using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.BL;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalesHistoriesController : ControllerBase
    {
        //private readonly SportsDbContext _context;

        //public SalesHistoriesController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/SalesHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesHistory>>> GetSalesHistories()
        {
            SalesHistoryBL salesHistory = new SalesHistoryBL();
            return Ok(salesHistory.GetSalesHistoryDetailsBL());
        }

        // GET: api/SalesHistories/5

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesHistory>> GetSalesHistory(int id)
        {
            SalesHistoryBL salesHistory = new SalesHistoryBL();
            var salesbyId = salesHistory.GetSalesHistoryBL(id);

            if (salesbyId == null)
            {
                return NotFound();
            }

            return salesbyId;
        }

        // POST: api/SalesHistories
        [HttpPost]
        public async Task<ActionResult<SalesHistory>> PostSalesHistory(SalesHistory salesHistory)
        {
            SalesHistoryBL sales = new SalesHistoryBL();
            sales.AddSalesHistoryBL(salesHistory);



            return CreatedAtAction("GetSalesHistory", new { id = salesHistory.SalesId }, salesHistory);
        }

        // PUT: api/SalesHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesHistory(int id, SalesHistory salesHistory)
        {
            SalesHistoryBL sales = new SalesHistoryBL();

            SportsStoreEnum salesEdit = sales.EditSalesHistoryBL(id, salesHistory);

            if (salesEdit == SportsStoreEnum.BadRequest)
            {
                return BadRequest();
            }

            if (salesEdit == SportsStoreEnum.NotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
