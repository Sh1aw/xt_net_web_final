using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface ICategoryLogic
    {
        int Add(string tag);
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        int Remove(int id);
        int Update(string tittle, int targetId);
    }
}
