using System;
using System.Collections.Generic;

namespace Tkachev_01.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Pasword { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
