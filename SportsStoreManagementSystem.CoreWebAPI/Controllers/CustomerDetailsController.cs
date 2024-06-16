using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.BL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.CoreWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        //private readonly SportsDbContext _context;

        //public CustomerDetailsController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/CustomerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetail>>> GetCustomerDetails()
        {
            CustomerDetailsBL customerDetails = new CustomerDetailsBL();
            return Ok(customerDetails.GetCustomerDetailsBL());
        }

        // GET: api/CustomerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetail>> GetCustomerDetail(int id)
        {
            CustomerDetailsBL customerDetails = new CustomerDetailsBL();

            var customerDetail = customerDetails.GetCustomerDetailBL(id);

            if (customerDetail == null)
            {
                return NotFound();
            }

            return customerDetail;
        }

        // POST: api/CustomerDetails
        [HttpPost]
        public async Task<ActionResult<CustomerDetail>> PostCustomerDetail(CustomerDetail customerDetail)
        {
            CustomerDetailsBL customerDetails = new CustomerDetailsBL();
            customerDetails.AddCustomerDetailBL(customerDetail);

            return CreatedAtAction("GetCustomerDetail", new { id = customerDetail.CustomerId }, customerDetail);
        }

        // PUT: api/CustomerDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetail(int id, CustomerDetail customerDetail)
        {
            CustomerDetailsBL customerDetails = new CustomerDetailsBL();
            SportsStoreEnum sportsStoreEnum = customerDetails.EditCustomerDetailBL(id, customerDetail);

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

        [HttpGet("contact/{contact}")]
        public async Task<ActionResult<CustomerDetail>> GetCustomerByContact(string contact)
        {
            CustomerDetailsBL customerDetails = new CustomerDetailsBL();

            var customerDetail = customerDetails.GetCustomerByContactBL(contact);
            if (customerDetail == null)
            {
                return NotFound();
            }
            return customerDetail;
        }
    }
}
