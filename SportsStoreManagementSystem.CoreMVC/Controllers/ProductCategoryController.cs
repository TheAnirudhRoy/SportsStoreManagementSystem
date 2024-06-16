using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductCategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44359/api/ProductCategory");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var productCategory = JsonConvert.DeserializeObject<List<ProductCategory>>(data) as IEnumerable<ProductCategory>;
            return View(productCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {

                var response = await _httpClient.PostAsJsonAsync("https://localhost:44359/api/ProductCategory", productCategory);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Category already exists");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

    }
}
