using System;
using System.Collections.Generic;

namespace Art_Gallery.DAL.Models;

public partial class ProductToCategory
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int TypeId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Category Type { get; set; } = null!;
}
