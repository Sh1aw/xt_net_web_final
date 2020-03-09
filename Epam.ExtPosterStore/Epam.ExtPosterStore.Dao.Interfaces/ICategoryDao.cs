using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface ICategoryDao
    {
        Category Add(Category tag);
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        int Remove(int id);
        Category Update(Category tag,int targetId);
    }
}
