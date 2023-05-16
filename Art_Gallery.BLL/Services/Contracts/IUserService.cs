using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;

namespace Art_Gallery.BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        User GetUserByEmail(string email);
        Task<User> Register(User u);
        int UserSignIn(string email, string password);
        Task<List<User>> GetAllUsers();
    }
}
