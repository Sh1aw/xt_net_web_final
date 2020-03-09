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
    public class OrderStateLogic : IOrderStateLogic
    {
        private readonly IOrderStateDao _orderStateDao;
        public OrderStateLogic(IOrderStateDao orderStateDao)
        {
            _orderStateDao = orderStateDao;
        }
        public IEnumerable<OrderState> GetAll()
        {
            return _orderStateDao.GetAll();
        }

        public OrderState GetById(int id)
        {
            return _orderStateDao.GetById(id);
        }
    }
}
