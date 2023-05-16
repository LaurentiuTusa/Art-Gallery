using System;
using System.Collections.Generic;

namespace Art_Gallery.DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double TotalPrice { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; } = null!;
}
