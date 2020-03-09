using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL.Interfaces
{
    public interface IUserLogic
    {
        int Add(string email,string password,string passwordCon, string firstName, string secondName,string phoneNumber);

        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByEmail(string email);

        int ChangeRole(int userId,string currentEmail,int roleId);
        bool Authorization(string email, string password);

        int UpdateUserInfo(string email, string firstName, string secondName, string phoneNumber);
        int UpdatePassword(string email,string oldPassword, string password, string passwordCon);
    }
}
