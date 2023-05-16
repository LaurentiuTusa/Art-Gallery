using System;
using System.Collections.Generic;

namespace Art_Gallery.DAL.Models;

public partial class Category
{
    public int Id { get; set; }

    public string ProductType { get; set; } = null!;

    public virtual ICollection<ProductToCategory> ProductToCategories { get; set; } = new List<ProductToCategory>();
}
