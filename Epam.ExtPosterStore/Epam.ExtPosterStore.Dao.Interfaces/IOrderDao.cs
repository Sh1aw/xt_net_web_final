using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface IOrderDao
    {
        int makeOrder(Order order, IEnumerable<CartPair> products);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersForUser(int userId);
        Order GetOrderById(int id);
        OrderState ChangeOrderState(int idOrder, OrderState orderState);
    }
}
