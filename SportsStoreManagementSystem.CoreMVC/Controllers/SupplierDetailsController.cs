using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class SupplierDetailsController : Controller
{
    private readonly HttpClient _httpClient;

    public SupplierDetailsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("https://localhost:44359/api/SupplierDetails");
        response.EnsureSuccessStatusCode();
        var data = await response.Content.ReadAsStringAsync();
        var suppliers = JsonConvert.DeserializeObject<List<SupplierDetail>>(data) as IEnumerable<SupplierDetail>;
        return View(suppliers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SupplierDetail supplierDetail)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:44359/api/SupplierDetails", supplierDetail);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
        return View(supplierDetail);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:44359/api/SupplierDetails/{id}");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        var supplier = JsonConvert.DeserializeObject<SupplierDetail>(data);
        return View(supplier);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SupplierDetail supplierDetail)
    {
        //if (id != supplierDetail.SupId)
        //{
        //    return BadRequest();
        //}

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:44359/api/SupplierDetails", supplierDetail);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
        return View(supplierDetail);
    }
}