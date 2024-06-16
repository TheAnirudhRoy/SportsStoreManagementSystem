using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class SalesItemDto
    {
        public int SalesId { get; set; }

        public int CustomerId { get; set; }

        public int? SupId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int PricePerItem { get; set; }
    }
}
