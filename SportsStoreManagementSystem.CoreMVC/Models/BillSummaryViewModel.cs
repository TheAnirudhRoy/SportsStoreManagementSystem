using SportsStoreManagementSystem.Entities;

namespace SportsStoreManagementSystem.CoreMVC.Models
{
    public class BillSummaryViewModel
    {
        public int SalesId { get; set; }
        public List<SalesItem>? SalesItems { get; set; }
    }
}
