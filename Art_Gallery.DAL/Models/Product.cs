using System;
using System.Collections.Generic;

namespace Art_Gallery.DAL.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ImgPath { get; set; } = null!;

    public string? Description { get; set; }

    public double Price { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ICollection<ProductToCategory> ProductToCategories { get; set; } = new List<ProductToCategory>();
}
