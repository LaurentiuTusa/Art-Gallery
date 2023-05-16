using System;
using System.Collections.Generic;

namespace Art_Gallery.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
