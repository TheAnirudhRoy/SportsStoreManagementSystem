using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProcurementDetailsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProcurementDetailsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44359/api/ProcurementDetails");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var procurement = JsonConvert.DeserializeObject<List<ProcurementDetail>>(data) as IEnumerable<ProcurementDetail>;
            return View(procurement);
        }

        public async Task<IActionResult> Create()
        {
            var supplierResponse = await _httpClient.GetAsync("https://localhost:44359/api/SupplierDetails");
            if (!supplierResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)supplierResponse.StatusCode, "Error fetching suppliers");
            }

            var supplierData = await supplierResponse.Content.ReadAsStringAsync();
            var suppliers = JsonConvert.DeserializeObject<List<SupplierDetail>>(supplierData);

            var productResponse = await _httpClient.GetAsync("https://localhost:44359/api/Products");
            if (!productResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)productResponse.StatusCode, "Error fetching products");
            }

            var productData = await productResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(productData);

            ViewData["SupId"] = new SelectList(suppliers, "SupId", "SubName");
            ViewData["ProductId"] = new SelectList(products, "ProductId", "ProductName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcurementDetail procurementDetail)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:44359/api/ProcurementDetails", procurementDetail);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(procurementDetail);
        }
    }
}
