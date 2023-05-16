using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;

namespace Art_Gallery.BLL.Services.Contracts
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsByCategory(int category_id);
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(Product product);
        void DetachProduct(Product product);

    }
}
