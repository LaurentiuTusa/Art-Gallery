using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;
using Art_Gallery.DAL.Repositories.Contracts;
using Art_Gallery.BLL.Services.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace Art_Gallery.BLL.Services
{
    public class ProductHistoryService : IProductHistoryService
    {
        public static Stack<Product> productsStack = new Stack<Product>();

/*        public ProductHistoryService()
        {
            this.productsStack = new Stack<Product>();
        }*/

        public Product stackPop()
        {
            //pop the stack
            try
            {
                return productsStack.Pop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void stackPush(Product p)
        {
            //add product to stack
            try
            {
                productsStack.Push(p);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int getCount()
        {
            //return count of stack
            try
            {
                return productsStack.Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
