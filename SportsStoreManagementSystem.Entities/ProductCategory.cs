using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStoreManagementSystem.Entities
{
    public partial class ProductCategory
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
