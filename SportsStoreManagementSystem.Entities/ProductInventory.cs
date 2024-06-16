using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class ProductInventory
    {
        public int SupId { get; set; }

        public int ProductId { get; set; }

        public int Stocks { get; set; }

        public virtual Product? Product { get; set; } = null!;

        public virtual SupplierDetail? Sup { get; set; } = null!;
    }
}
