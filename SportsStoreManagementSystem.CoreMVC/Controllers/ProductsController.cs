using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;
using System.Net.Http;
//using SportsStoreManagementSystem.DAL.Models;

namespace SportsStoreManagementSystem.CoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        //SportsDbContext db = new SportsDbContext();

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44359/api/Products");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(data) as IEnumerable<Product>;
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categoryResponse = await _httpClient.GetAsync("https://localhost:44359/api/ProductCategory");
            if (!categoryResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)categoryResponse.StatusCode, "Error fetching categories");
            }

            var categoryData = await categoryResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoryData);

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44359/api/Products", product);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
