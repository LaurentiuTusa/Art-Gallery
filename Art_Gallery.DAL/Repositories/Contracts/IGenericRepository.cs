using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Art_Gallery.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        //User
        Task<User> GetUserById(int id);
        User GetUserByEmail(string email);
        Task<User> Register(User u);
        Task<List<TModel>> GetAllUsers();

        //Category
        Task<List<TModel>> GetCategories();
        Task<Category> CreateCategory(Category category);

        //Product
        Task<List<TModel>> GetProducts();
        Task<List<Product>> GetProductsByProductIds(List<int> productIds);
        Task<Product> AddProduct(Product p);
        Task<Product> GetProductById(int id);
        Task<Product> UpdateProduct(Product p);
        void DetachProduct(Product product);

        //Order

        Task<List<TModel>> GetOrders();
        Task<Order> AddOrder(Order o);
        Task<List<Order>> GetMyOrdersByUserId(int userId);

        //ProductToCategory

        Task<List<ProductToCategory>> GetProductToCategoriesByTypeId(int typeId);
        Task<ProductToCategory> AddProductToCategory(ProductToCategory ptc);
    }
}
