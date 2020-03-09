using System;
using System.Collections.Generic;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.Dao.Interfaces;
using Epam.ExtPosterStore.Entities;

namespace Epam.ExtPosterStore.BLL
{
    public class RoleLogic : IRoleLogic
    {
        private readonly IRoleDao _roleDao;

        public RoleLogic(IRoleDao roleDao)
        {
            _roleDao = roleDao;
        }
        public Role Add(Role role)
        {
            return _roleDao.Add(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleDao.GetAll();
        }

        public Role GetById(int id)
        {
            return _roleDao.GetById(id);
        }

        public int Remove(int id)
        {
            return _roleDao.Remove(id);
        }

        public Role Update(Role role)
        {
            return _roleDao.Update(role);
        }
    }
}
