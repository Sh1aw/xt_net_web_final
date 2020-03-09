using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface IProductDao
    {
        Product Add(Product product);

        int Delete(int id);

        Product Update(Product product, int targetId);

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        int ChangeVisibility(int id, bool visibility);
    }
}
