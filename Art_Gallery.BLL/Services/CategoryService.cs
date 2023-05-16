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
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }
        //METHODS IMPLEMENTATIONS
        public async Task<List<Category>> GetCategories()
        {
            try
            {
                return await _repository.GetCategories();
            }
            catch (Exception)
            {
                throw;
            }
        }  
        
        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                return await _repository.CreateCategory(category);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
