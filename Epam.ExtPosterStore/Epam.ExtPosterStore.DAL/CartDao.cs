using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.DAL
{
    public class CartDao : ICartDao
    {
        List<CartPair> cartList = new List<CartPair>();
        public void Add(int id)
        {
            if (cartList.Any(c => c.IdProduct.Equals(id)))
            {
                cartList.First(c => c.IdProduct.Equals(id)).CountProduct++;
            }
            else
            {
                cartList.Add(new CartPair(id));
            }
        }

        public IEnumerable<CartPair> GetAll()
        {
            return cartList;
        }

        public int GetAllCountPositions()
        {
            int count  = 0;
            foreach (var pair in cartList)
            {
                count += pair.CountProduct;
            }

            return count;
        }

        public void Remove(int id)
        {
            if (cartList.Any(c=>c.IdProduct.Equals(id)))
            {
                var current = cartList.First(c => c.IdProduct.Equals(id));
                cartList.Remove(current);
            }
        }

        public void Clear()
        {
            if (cartList.Any())
            {
                cartList.Clear();
            }
        }
    }
}
