using System;
using System.Collections.Generic;

namespace Tkachev_01.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public double Price { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
