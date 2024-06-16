using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SportsStoreManagementSystem.Entities;

public partial class CustomerDetail
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerContact { get; set; } = null!;

    public virtual ICollection<SalesHistory> SalesHistories { get; set; } = new List<SalesHistory>();

    public virtual ICollection<SalesItem> SalesItems { get; set; } = new List<SalesItem>();
}

