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
    public class ProductToCategoryService : IProductToCategoryService
    {
        private readonly IGenericRepository<ProductToCategory> _repository;
        public ProductToCategoryService(IGenericRepository<ProductToCategory> repository)
        {
            _repository = repository;
        }
        //METHODS IMPLEMENTATIONS
        public async Task<ProductToCategory> AddProductToCategory(ProductToCategory productToCategory)
        {
            try
            {
                return await _repository.AddProductToCategory(productToCategory);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
