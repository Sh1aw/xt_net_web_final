using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDao _orderDao;
        private readonly IOrderStateDao _orderStateDao;
        private readonly IPayDao _payDao;
        private readonly IUserDao _userDao;

        public OrderLogic(IOrderDao orderDao, IOrderStateDao oreStateDao, IPayDao payDao, IUserDao userDao)
        {
            _orderDao = orderDao;
            _orderStateDao = oreStateDao;
            _payDao = payDao;
            _userDao = userDao;
        }

        public OrderState AbortOrder(int idOrder, string userEmail)
        {
            var order = _orderDao.GetOrderById(idOrder);
            var user = _userDao.GetByEmail(userEmail);
            OrderState abordState = null;
            if (order!=null && user!=null)
            {
                if (user.Id == order.UserId)
                {
                    var orderState = _orderStateDao.GetById(5);
                    abordState = _orderDao.ChangeOrderState(idOrder,orderState);
                }
            }
            return abordState;
        }

        public OrderState ChangeState(int idOrder, int idState)
        {
            var order = _orderDao.GetOrderById(idOrder);
            var state = _orderStateDao.GetById(idState);
            if (order != null && state != null)
            {
                state = _orderDao.ChangeOrderState(idOrder, state);
            }
            return state;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderDao.GetAllOrders();
        }

        public IEnumerable<Order> GetAllOrdersForUser(int userId)
        {
            return _orderDao.GetAllOrdersForUser(userId);
        }

        public Order GetOrderById(int id)
        {
            return _orderDao.GetOrderById(id);
        }

        public int makeOrder(string userEmail, int stateId, decimal finalCost,int payId, Address address, IEnumerable<CartPair> cartItems)
        {
            int response = 400;
            var state = _orderStateDao.GetById(stateId);
            var pay = _payDao.GetById(payId);
            var user = _userDao.GetByEmail(userEmail);
            if (user!=null && cartItems!=null && cartItems.Any())
            {
                try
                {
                    response = _orderDao.makeOrder(new Order(DateTime.Now, user.Id,state,finalCost,pay,address),cartItems);
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    response = 400;
                }
            }
            return response;
        }
    }
}
