using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface IOrderLogic
    {
        int makeOrder(string userEmail, int stateId, decimal finalCost, int payId, Address address, IEnumerable<CartPair> cartItems);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersForUser(int userId);
        Order GetOrderById(int id);

        OrderState ChangeState(int idOrder, int idState);
        OrderState AbortOrder(int idOrder, string userEmail);
    }
}
