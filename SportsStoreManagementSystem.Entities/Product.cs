using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class Product
    {
        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int Price { get; set; }

        [JsonIgnore]
        public virtual ProductCategory? Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProcurementDetail> ProcurementDetails { get; set; } = new List<ProcurementDetail>();

        [JsonIgnore]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        [JsonIgnore]
        public virtual ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
}
