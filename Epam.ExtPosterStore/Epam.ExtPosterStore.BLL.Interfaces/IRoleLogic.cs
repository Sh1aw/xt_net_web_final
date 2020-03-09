using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface IRoleLogic
    {
        Role Add(Role role);
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        int Remove(int id);

        Role Update(Role role);
    }
}
