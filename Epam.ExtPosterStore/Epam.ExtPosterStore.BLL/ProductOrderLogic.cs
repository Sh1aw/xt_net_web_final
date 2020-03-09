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
    public class ProductOrderLogic : IProductOrderLogic
    {
        private readonly IProductOrderDao _productOrderDao;

        public ProductOrderLogic(IProductOrderDao productOrderDao)
        {
            _productOrderDao = productOrderDao;
        }
        public IEnumerable<ProductOrder> GetAll()
        {
            return _productOrderDao.GetAll();
        }

        public IEnumerable<CartPair> GetAllProductForOrder(int idOrder)
        {
            return _productOrderDao.GetAllProductForOrder(idOrder);
        }
    }
}
