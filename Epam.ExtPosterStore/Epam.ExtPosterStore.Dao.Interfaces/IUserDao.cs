using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.Dao.Interfaces
{
    public interface IUserDao
    {
        User Add(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);
        int Remove(int id);
        User Update(User user, int targetId);
        int ChangeRole(Role role, int userId);
        int UpdatePassword(string newPassword, int targetId);
    }
}
