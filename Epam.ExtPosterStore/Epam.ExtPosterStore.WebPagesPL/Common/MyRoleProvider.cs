using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Epam.ExtPosterStore.BLL.Interfaces;
using Epam.ExtPosterStore.BLL.Ioc;

namespace Epam.ExtPosterStore.WebPagesPL.Common
{
    public class MyRoleProvider : RoleProvider
    {

        private IUserLogic _userLogic = DependencyResolver.UserLogic;
        private IRoleLogic _roleLogic = DependencyResolver.RoleLogic;

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _userLogic.GetByEmail(username);
            if (user != null && user.Role != null)
            {
                var visitorRole = _roleLogic.GetById(user.Role.Id);
                if (visitorRole.Name.Equals(roleName))
                {
                    return true;
                }

                return false;
            }

            return false;

        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _userLogic.GetByEmail(username);
            if (user != null && user.Role != null)
            {
                var role = _roleLogic.GetById(user.Role.Id);
                return new string[] { role.Name };
            }
            else
            {
                return new string[] { };
            }
        }

        #region NotImplemented

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}