using System.Collections.Generic;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class CartLogic : ICartLogic
    {
        private readonly ICartDao _cartDao;
        private readonly IProductDao _productDao;
        public CartLogic(ICartDao cartDao, IProductDao productDao)
        {
            _cartDao = cartDao;
            _productDao = productDao;
        }
        public bool Add(int id)
        {
            var product = _productDao.GetById(id);
            if (product!=null && product.Visibility)
            {
                _cartDao.Add(id);
                return true;
            }

            return false;
        }

        public IEnumerable<CartPair> GetAll()
        {
            return _cartDao.GetAll();
        }

        public int GetAllCountPositions()
        {
            return _cartDao.GetAllCountPositions();
        }

        public decimal GetFinalCost()
        {
            decimal finalPrice = 0;
            var allItems = _cartDao.GetAll();
            foreach (var pair in allItems)
            {
                finalPrice += _productDao.GetById(pair.IdProduct).Price * pair.CountProduct;
            }

            return finalPrice;
        }

        public void Remove(int id)
        {
            _cartDao.Remove(id);
        }
        public void Clear()
        {
            _cartDao.Clear();
        }

    }
}
