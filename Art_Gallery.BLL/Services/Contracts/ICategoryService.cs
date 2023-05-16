using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;

namespace Art_Gallery.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> CreateCategory(Category category);
    }
}
