using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;
using Art_Gallery.DAL.Repositories.Contracts;
using Art_Gallery.BLL.Services.Contracts;

namespace Art_Gallery.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;
        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }
        //METHODS IMPLEMENTATIONS
        public async Task<List<Product>> GetProducts()// ALL
        {
            try
            {
                return await _repository.GetProducts();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> GetProductsByCategory(int category_id)
        {
            try
            {
                List<ProductToCategory> l = await _repository.GetProductToCategoriesByTypeId(category_id); //lista de entries din tabel ProductToCategory
                List<int> ids = new List<int>();
                foreach (ProductToCategory p in l)
                {
                    ids.Add(p.ProductId);
                }

                return await _repository.GetProductsByProductIds(ids);
                //return await _repository.GetProductsByCategory(category_id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> CreateProduct(Product product)
        {
            try
            {
                return await _repository.AddProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                return await _repository.GetProductById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                return await _repository.UpdateProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DetachProduct(Product product)
        {
            try
            {
                _repository.DetachProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
