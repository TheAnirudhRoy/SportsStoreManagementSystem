using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreMVC.Models
{
    public class BillingViewModel
    {
        public CustomerDetail? Customer { get; set; }
        public List<ProductCategory>? Categories { get; set; }
        public List<Product>? Products { get; set; }
        public int? SelectedCustomerId { get; set; }
    }
}
