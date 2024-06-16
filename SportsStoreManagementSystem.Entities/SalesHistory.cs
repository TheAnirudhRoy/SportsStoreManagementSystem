using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class SalesHistory
    {
        public int SalesId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public int? BillTotal { get; set; }

        public virtual CustomerDetail? Customer { get; set; }
    }
}
