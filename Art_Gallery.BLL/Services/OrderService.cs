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
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _repository;
        public OrderService(IGenericRepository<Order> repository)
        {
            _repository = repository;
        }
        //METHODS IMPLEMENTATIONS
        public async Task<List<Order>> GetOrders()
        {
            try
            {
                return await _repository.GetOrders();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Order> AddOrder(Order o)
        {
            try
            {
                return await _repository.AddOrder(o);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Order>> GetMyOrdersByUserId(int userId)
        {
            try
            {
                return await _repository.GetMyOrdersByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
