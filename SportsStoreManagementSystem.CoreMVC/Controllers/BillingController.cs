using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SportsStoreManagementSystem.Entities;
using SportsStoreManagementSystem.CoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Net.Http;

[Authorize(Roles = "Clerk")]
public class BillingController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BillingController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(string id)
    {
        var client = _httpClientFactory.CreateClient();

        var customerResponse = await client.GetAsync($"https://localhost:44359/api/CustomerDetails/contact/{id}");
        if (!customerResponse.IsSuccessStatusCode)
        {
            return NotFound("Customer not found");
        }

        var customerData = await customerResponse.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDetail>(customerData);

        var categoryResponse = await client.GetAsync("https://localhost:44359/api/ProductCategory");
        if (!categoryResponse.IsSuccessStatusCode)
        {
            return StatusCode((int)categoryResponse.StatusCode, "Error fetching categories");
        }

        var categoryData = await categoryResponse.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<List<ProductCategory>>(categoryData);

        var model = new BillingViewModel
        {
            Customer = customer,
            Categories = categories
        };

        return View(model);
    }

    public ActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(CustomerDetail customerDetail)
    {
        var client = _httpClientFactory.CreateClient();

        var customerResponse = await client.GetAsync($"https://localhost:44359/api/CustomerDetails/contact/{customerDetail.CustomerContact}");
        if (!customerResponse.IsSuccessStatusCode)
        {
            return RedirectToAction("Create");
        }

        var customerData = await customerResponse.Content.ReadAsStringAsync();
        var customer = JsonConvert.DeserializeObject<CustomerDetail>(customerData);
        return RedirectToAction("Index", new { id = customer.CustomerContact });
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CustomerDetail customerDetail)
    {
        if (ModelState.IsValid)
        {
            var client = _httpClientFactory.CreateClient();

            var customerResponse = await client.PostAsJsonAsync("https://localhost:44359/api/CustomerDetails", customerDetail);

            if (!customerResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)customerResponse.StatusCode, "Customer already exists");
            }

            return RedirectToAction("Index", new { id = customerDetail.CustomerContact });
        }
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> GetProductsByCategory(int categoryId)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44359/api/Products/byCategory/{categoryId}");

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Error fetching products" });
        }

        var data = await response.Content.ReadAsStringAsync();
        var products = JsonConvert.DeserializeObject<List<Product>>(data);

        if (products == null || !products.Any())
        {
            return Json(new { success = false, message = "No products found" });
        }
        return Json(new { success = true, products });
    }

    [HttpGet]
    public async Task<JsonResult> GetSuppliersByProductId(int productId)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44359/api/SupplierDetails/byProduct/{productId}");

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Error fetching suppliers" });
        }

        var data = await response.Content.ReadAsStringAsync();
        var suppliers = JsonConvert.DeserializeObject<List<SupplierDetail>>(data);

        if (suppliers == null || !suppliers.Any())
        {
            return Json(new { success = false, message = "No Suppliers found" });
        }

        return Json(suppliers);
    }

    [HttpGet]
    public async Task<JsonResult> GetProductStocks(int productID, int supID)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44359/api/ProductInventory/{supID}/{productID}");

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Error fetching inventory" });
        }

        var data = await response.Content.ReadAsStringAsync();
        var inventory = JsonConvert.DeserializeObject<ProductInventory>(data);

        if (inventory == null)
        {
            return Json(new { success = false, message = "No Inventory found" });
        }

        return Json(inventory);
    }

    [HttpPost]
    public async Task<JsonResult> Checkout([FromBody] List<SalesItemViewModel> items)
    {
        if (items == null || !items.Any())
        {
            return Json("No items to checkout");
        }

        var client = _httpClientFactory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(items), System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:44359/api/SalesItems", content);

        if (!response.IsSuccessStatusCode)
        {
            return Json((int)response.StatusCode, "Error placing order");
        }

        var salesId = await response.Content.ReadAsStringAsync(); // Assuming the salesId is returned as a string

        return Json(salesId);
    }

    public async Task<IActionResult> BillSummary(int id)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync($"https://localhost:44359/api/SalesItems/SalesId/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return NotFound("Sales record not found");
        }

        var data = await response.Content.ReadAsStringAsync();
        var salesItems = JsonConvert.DeserializeObject<List<SalesItem>>(data);

        return View(new BillSummaryViewModel
        {
            SalesId = id,
            SalesItems = salesItems
        });
    }

    public IActionResult Return()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderItems(int orderId)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44359/api/SalesItems/SalesId/{orderId}");

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { success = false, message = "Order not found" });
        }

        var data = await response.Content.ReadAsStringAsync();
        var orderItems = JsonConvert.DeserializeObject<List<SalesItem>>(data);

        return Json(new { success = true, orderItems });
    }

    [HttpPost]
    public async Task<JsonResult> ReturnItems([FromBody] List<ReturnItemDto> items)
    {
        if (items == null || !items.Any())
        {
            return Json("No items to Return");
        }

        var client = _httpClientFactory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(items), System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:44359/api/SalesItems/Return", content);

        if (!response.IsSuccessStatusCode)
        {
            return Json((int)response.StatusCode, "Error returning order");
        }

        var salesId = await response.Content.ReadAsStringAsync(); // Assuming the salesId is returned as a string

        return Json(salesId);
    }
}