using System;
using System.Collections.Generic;

namespace Tkachev_01.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int FcustomerId { get; set; }

    public int FproductId { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Customer Fcustomer { get; set; } = null!;

    public virtual Product Fproduct { get; set; } = null!;
}
