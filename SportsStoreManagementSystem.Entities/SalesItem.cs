using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class SalesItem
    {
        public int SalesId { get; set; }

        public int CustomerId { get; set; }

        public int? SupId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int PricePerItem { get; set; }

        public virtual CustomerDetail? Customer { get; set; }

        public virtual Product? Product { get; set; }

        public virtual SupplierDetail? Sup { get; set; }
    }
}
