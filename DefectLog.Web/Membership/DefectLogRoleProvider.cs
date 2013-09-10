using System;
using System.Web.Mvc;
using System.Web.Security;
using DefectLog.Core.Services;

namespace DefectLog.Web.Membership
{
    public class DefectLogRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string rolename)
        {
            return DependencyResolver.Current.GetService<IUserService>().IsUserInRole(username, rolename);
        }

        public override string[] GetRolesForUser(string username)
        {
            return DependencyResolver.Current.GetService<IUserService>().GetRolesForUser(username);
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}