using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface IOrderStateDao
    {
        IEnumerable<OrderState> GetAll();
        OrderState GetById(int id);
    }
}
