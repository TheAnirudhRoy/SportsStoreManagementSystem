using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class ReturnItemDto
    {
        public int SalesId { get; set; }

        public int? SupId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }
    }
}