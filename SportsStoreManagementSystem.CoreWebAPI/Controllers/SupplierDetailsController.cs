using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.BL;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierDetailsController : ControllerBase
    {
        //private readonly SportsDbContext _context;

        //public SupplierDetailsController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/SupplierDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDetail>>> GetSupplierDetails()
        {
            SupplierDetailsBL supplierDetails = new SupplierDetailsBL();

            return Ok(supplierDetails.GetSupplierDetailsBL());
        }

        // GET: api/SupplierDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDetail>> GetSupplierDetail(int id)
        {
            SupplierDetailsBL supplierDetails = new SupplierDetailsBL();

            var supplierDetail =  supplierDetails.GetSupplierDetailBL(id);

            if (supplierDetail == null)
            {
                return NotFound();
            }

            return supplierDetail;
        }

        // POST: api/SupplierDetails
        [HttpPost]
        public async Task<ActionResult<SupplierDetail>> PostSupplierDetail(SupplierDetail supplierDetail)
        {
            SupplierDetailsBL supplierDetails = new SupplierDetailsBL();

            supplierDetails.AddSupplierDetailBL(supplierDetail);


            return CreatedAtAction("GetSupplierDetail", new { id = supplierDetail.SupId }, supplierDetail);
        }

        // PUT: api/SupplierDetails/5
        [HttpPut]
        public async Task<IActionResult> PutSupplierDetail(SupplierDetail supplierDetail)
        {
            SupplierDetailsBL supplierDetails = new SupplierDetailsBL();
            SportsStoreEnum sportsStoreEnum = supplierDetails.EditSupplierDetailBL(supplierDetail);

            if(sportsStoreEnum == SportsStoreEnum.NotFound)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpGet("byProduct/{productId}")]
        public async Task<ActionResult<IEnumerable<SupplierDetail>>> GetSupplierDetailsByProductId(int productId)
        {

            SupplierDetailsBL supplierDetails = new SupplierDetailsBL();



            var suppliers = supplierDetails.GetSupplierDetailsByProductIdBL(productId);

            if (!suppliers.Any())
            {
                return NotFound();
            }

            return suppliers.ToList();
        }

        
    }
}