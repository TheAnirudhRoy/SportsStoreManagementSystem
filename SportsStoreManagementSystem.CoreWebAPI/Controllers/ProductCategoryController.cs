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
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryBL productCategoryObj = new ProductCategoryBL();

        //private readonly SportsDbContext _context;

        //public ProductCategoryController(SportsDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetAllProductCategories()
        {
            return Ok(productCategoryObj.GetAllProductCategoriesBL());
        }

        // POST: api/ProductCategories
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            bool categoryAdded = productCategoryObj.AddProductCategoryBL(productCategory);
            if (categoryAdded)
                return Ok();
            else
                return BadRequest();
            //return CreatedAtAction("GetAllProductCategories", new { id = productCategory.CategoryId }, productCategory);
        }
    }
}
