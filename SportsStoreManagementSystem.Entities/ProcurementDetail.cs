using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class ProcurementDetail
    {
        public int ProcurementId { get; set; }

        public int SupId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int? TotalPrice { get; set; }

        public virtual Product? Product { get; set; }

        public virtual SupplierDetail? Sup { get; set; }
    }
}
