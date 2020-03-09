using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface ITypeOfProductLogic
    {
        int Add(string tittle);
        int Remove(int id);
        IEnumerable<TypeOfProduct> GetAll();
        TypeOfProduct GetById(int id);
        int Update(string tittle, int targetId);
    }
}
