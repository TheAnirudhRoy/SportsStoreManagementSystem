using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportsStoreManagementSystem.BL;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementDetailsController : ControllerBase
    {
        readonly ProcurementDetailsBL procureObj = new ProcurementDetailsBL();

        //private readonly SportsDbContext _context;

        //public ProcurementDetailsController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/ProcurementDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcurementDetail>>> GetAllProcurementDetails()
        {
            return Ok(procureObj.GetAllProcurementDetailsBL());
        }

        // POST: api/ProcurementDetails
        [HttpPost]
        public async Task<ActionResult<ProcurementDetail>> PostProcurementDetail(ProcurementDetail procurementDetail)
        {
            bool procureAdded = procureObj.AddProcurementDetailBL(procurementDetail);
            if (procureAdded)
                return Ok();
            else
                return BadRequest();
            //return CreatedAtAction("GetAllProcurementDetails", new { id = procurementDetail.ProcurementId }, procurementDetail);
        }
    }
}

