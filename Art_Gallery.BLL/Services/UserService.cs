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
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;

        public UserService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        //METHODS IMPLEMENTATIONS
        public async Task<User> GetUserById(int id)
        {
            return await _repository.GetUserById(id);
        }
        public User GetUserByEmail(string email)
        {
            return _repository.GetUserByEmail(email);
        }

        public static string Base64Encode(string s)
        {
            var sBytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(sBytes);
        }

        public async Task<User> Register(User u)
        {
            try
            {
                u.Password = Base64Encode(u.Password);
                u.IsAdmin = false;
                return await _repository.Register(u);
            }
            catch
            {
                throw;
            }
        }

        public int UserSignIn(string email, string password)
        {
            var user = _repository.GetUserByEmail(email);

            if (user == null)
            {
                return 0;
            }
            else if (Base64Encode(password) != user.Password)
            {
                return 0;
            }
            else if(user.IsAdmin == true)
            { 
                return 1; //ADMIN
            }
            else
            {
                return 2; //USER
            }
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await _repository.GetAllUsers();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
