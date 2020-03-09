using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface IProductOrderLogic
    {
        IEnumerable<CartPair> GetAllProductForOrder(int idOrder);
        IEnumerable<ProductOrder> GetAll();
    }
}
