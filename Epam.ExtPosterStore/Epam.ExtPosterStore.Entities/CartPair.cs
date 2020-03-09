using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class CartPair
    {
        public int IdProduct { get; set; }
        public int CountProduct { get; set; }

        public CartPair(int id)
        {
            IdProduct = id;
            CountProduct = 1;
        }
        public CartPair(int id, int count)
        {
            IdProduct = id;
            CountProduct = count;
        }
    }
}

