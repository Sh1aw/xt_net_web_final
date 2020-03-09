using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface ITypeOfProductDao
    {
        TypeOfProduct Add(TypeOfProduct type);
        int Remove(int id);
        IEnumerable<TypeOfProduct> GetAll();
        TypeOfProduct GetById(int id);
        TypeOfProduct Update(TypeOfProduct type, int targetId);
    }
}
