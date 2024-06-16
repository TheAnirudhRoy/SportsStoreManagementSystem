using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class SupplierDetail
    {
        public int SupId { get; set; }

        public string? SubName { get; set; }

        public string? SubAddress { get; set; }

        public string? SubContact { get; set; }

        public virtual ICollection<ProcurementDetail> ProcurementDetails { get; set; } = new List<ProcurementDetail>();

        [JsonIgnore]
        public virtual ICollection<ProductInventory> ProductInventories { get; set; } = new List<ProductInventory>();

        public virtual ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
    }
}

