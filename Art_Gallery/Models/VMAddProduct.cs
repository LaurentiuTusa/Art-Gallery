namespace Art_Gallery.Models
{
    public class VMAddProduct
    {
        public string Title { get; set; }

        public IFormFile ImgPath { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }
        public List<int> Categories { get; set; } // Property for selected category IDs
    }
}
