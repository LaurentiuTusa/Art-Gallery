using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Art_Gallery.DAL.Models;

namespace Art_Gallery.BLL.Services.Contracts
{
    public interface IProductHistoryService
    {
        Product stackPop();
        void stackPush(Product p);
        int getCount();
    }
}
