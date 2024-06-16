using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductInventoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductInventoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44359/api/ProductInventory");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var pin = JsonConvert.DeserializeObject<List<ProductInventory>>(data) as IEnumerable<ProductInventory>;
            return View(pin);
        }

        public async Task<IActionResult> Edit(int id, int id2)
        {
            var response = await _httpClient.GetAsync($"https://localhost:44359/api/ProductInventory/{id}/{id2}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var pinv = JsonConvert.DeserializeObject<ProductInventory>(data);
            TempData["stock"] = pinv.Stocks;
            return View(pinv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductInventory productInventory)
        {
            //if (id != supplierDetail.SupId)
            //{
            //    return BadRequest();
            //}
            int stock = Convert.ToInt32(TempData["stock"]);
            productInventory.Stocks = stock - productInventory.Stocks;
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:44359/api/ProductInventory", productInventory);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(productInventory);
        }
    }
}
