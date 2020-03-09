using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.ExtPosterStore.Entities
{
    public class ProductOrder
    {
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }
        public int Count { get; set; }

        public ProductOrder(int idProduct, int idOrder, int count)
        {
            IdProduct = idProduct;
            IdOrder = idOrder;
            Count = count;
        }
    }
}
