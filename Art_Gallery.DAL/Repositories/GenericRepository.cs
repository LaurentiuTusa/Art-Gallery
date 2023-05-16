using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.DataContext;
using Art_Gallery.DAL.Models;
using Art_Gallery.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Art_Gallery.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        private readonly ArtGalleryContext _DBcontext;
        public GenericRepository(ArtGalleryContext DBcontext)
        {
            _DBcontext = DBcontext;
        }

        //METHODS IMPLEMENTATIONS
        //User
        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _DBcontext.Users.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public User GetUserByEmail(string email)
        {
            return _DBcontext.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User> Register(User u)
        {
            await _DBcontext.Users.AddAsync(u);
            await _DBcontext.SaveChangesAsync();
            return u;
        }

        public async Task<List<TModel>> GetAllUsers()
        {
            try
            {
                return await _DBcontext.Set<TModel>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Category
        public async Task<List<TModel>> GetCategories()
        {
            try
            {
                return await _DBcontext.Set<TModel>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _DBcontext.Categories.AddAsync(category);
            await _DBcontext.SaveChangesAsync();
            return category;
        }

        //Product
        public async Task<List<TModel>> GetProducts() // ALL
        {
            try
            {
                return await _DBcontext.Set<TModel>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByProductIds(List<int> productIds)
        {
            List<Product> products = await _DBcontext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();

            return products;
        }

        public async Task<Product> AddProduct(Product p)
        {
            await _DBcontext.Products.AddAsync(p);
            await _DBcontext.SaveChangesAsync();
            return p;
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _DBcontext.Products.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Product> UpdateProduct(Product p)
        {
            _DBcontext.Products.Update(p);
            await _DBcontext.SaveChangesAsync();
            return p;
        }

        public void DetachProduct(Product product)
        {
            _DBcontext.Entry(product).State = EntityState.Detached;
        }

        //Order

        public async Task<List<TModel>> GetOrders()
        {
            try
            {
                return await _DBcontext.Set<TModel>().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetMyOrdersByUserId(int userId)
        {
            List<Order> orders = await _DBcontext.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
            return orders;
        }

        public async Task<Order> AddOrder(Order o)
        {
            await _DBcontext.Orders.AddAsync(o);
            await _DBcontext.SaveChangesAsync();
            return o;
        }

        //ProductToCategory


        public async Task<List<ProductToCategory>> GetProductToCategoriesByTypeId(int typeId)
        {
            List<ProductToCategory> productToCategories = await _DBcontext.ProductToCategories
                .Where(ptc => ptc.TypeId == typeId)
                .ToListAsync();

            return productToCategories;
        }

        public async Task<ProductToCategory> AddProductToCategory(ProductToCategory ptc)
        {
            await _DBcontext.ProductToCategories.AddAsync(ptc);
            await _DBcontext.SaveChangesAsync();
            return ptc;
        }

    }
}
