using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Epam.ExtPosterStore.BLL.Common;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        private readonly IRoleDao _roleDao;

        public UserLogic(IUserDao userDao, IRoleDao roleDao)
        {
            _userDao = userDao;
            _roleDao = roleDao;
        }
        public int Add(string email, string password, string passwordCon, string firstName, string secondName, string phoneNumber)
        {
            if ((String.IsNullOrWhiteSpace(password)||string.IsNullOrWhiteSpace(passwordCon)) || !password.Equals(passwordCon))
            {
                return 400;
            }
            email = email.ToLower();
            var current = _userDao.GetByEmail(email);
            if (current!=null)
            {
                return 302; //this user already register
            }
            password = PasswordEncrypt.GetMD5HashData(password);
            try
            {
                var currnet = _userDao.Add(new User(email, password, firstName, secondName, phoneNumber, _roleDao.GetById(1)));
                if (currnet != null)
                {
                    return 200;
                }
                return 500;
            }
            catch (ArgumentException e)
            {
                Logger.Logger.InitLogger();
                Logger.Logger.Log.Error(e);
                return 400; //Wrong value of user props
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _userDao.GetAll();
        }
        public User GetById(int id)
        {
            return _userDao.GetById(id);
        }

        public User GetByEmail(string email)
        {
            return _userDao.GetByEmail(email.ToLower());
        }

        public bool Authorization(string email, string password)
        {
            email = email.Trim().ToLower();
            var current = _userDao.GetByEmail(email);
            if (current!=null)
            {
                if (PasswordEncrypt.checkHashPassword(current.Password, password))
                {
                    FormsAuthentication.SetAuthCookie(email, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public int UpdateUserInfo(string email,string firstName, string secondName, string phoneNumber)
        {
            var currentUser = _userDao.GetByEmail(email);
            if (currentUser != null)
            {
                try
                {
                    currentUser.Name = firstName;
                    currentUser.Surname = secondName;
                    currentUser.PhoneNumber = phoneNumber;
                    var updated = _userDao.Update(currentUser, currentUser.Id);
                    if (updated != null)
                    {
                        return 200;
                    }
                }
                catch (ArgumentException e)
                {
                    Logger.Logger.InitLogger();
                    Logger.Logger.Log.Error(e);
                    return 400;
                }
                
            }
            return 404;
        }

        public int UpdatePassword(string email, string oldPassword, string password, string passwordCon)
        {
            if ((String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordCon)
                || String.IsNullOrWhiteSpace(oldPassword)) || !password.Equals(passwordCon))
            {
                return 400;
            }
            var currentUser = _userDao.GetByEmail(email);
            if (currentUser!=null)
            {
                if (PasswordEncrypt.checkHashPassword(currentUser.Password,oldPassword))
                {
                    password = PasswordEncrypt.GetMD5HashData(password);
                    return _userDao.UpdatePassword(password, currentUser.Id);
                }
            }
            return 404;
        }

        public int ChangeRole(int userId,string currentEmail, int roleId)
        {
            int response = 400;
            var user = _userDao.GetById(userId);
            var current = _userDao.GetByEmail(currentEmail);
            var role = _roleDao.GetById(roleId);
            if (user!=null && current!=null && role!=null)
            {
                if (user.Id == current.Id)
                {
                    response = 302;
                }
                else
                {
                    response = _userDao.ChangeRole(role, userId);
                }
            }
            return response;
        }
    }
}
