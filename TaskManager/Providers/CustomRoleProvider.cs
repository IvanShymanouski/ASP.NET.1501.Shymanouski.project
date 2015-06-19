using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BLL.Interfaces;
using TaskManager.Infrastructure;

namespace TaskManager.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string emailOrLogin)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));            

            UserEntity user = users.Find(x => x.Email == emailOrLogin || x.Login == emailOrLogin);
            string[] role = new string[] { };
            if (null != user)
            {
                int i = 0;
                while (i < RoleKeysNames.keys.Length && RoleKeysNames.keys[i] != user.RoleId ) i++;
                if (i == RoleKeysNames.keys.Length) throw new ArgumentException("User " + user.Id + " has invalid roleId :" + user.RoleId);
                role = new String[] { RoleKeysNames.names[i] };
            }
            return role;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string emailOrLogin, string roleName)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));

            bool inRole = false;

            UserEntity user = users.Find(x => x.Email == emailOrLogin || x.Login == emailOrLogin);            
            if (null != user)
            {
                int i = 0;
                while (i < RoleKeysNames.keys.Length && RoleKeysNames.keys[i] != user.RoleId ) i++;
                if (i == RoleKeysNames.keys.Length) throw new ArgumentException("User " + user.Id + " has invalid roleId :" + user.RoleId);
                if (RoleKeysNames.names[i] == roleName) inRole = true;
            }
            return inRole;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();          
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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
            return RoleKeysNames.names;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            int i=0;
            while (i < RoleKeysNames.names.Length && RoleKeysNames.names[i] != roleName) i++;
            if (i==RoleKeysNames.names.Length) throw new ArgumentException("Role " + roleName + " not exist");

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            var matchUsers = users.GetAll().Where(x => x.RoleId == RoleKeysNames.keys[i]);
            List<string> listUsers = new List<string>();
            foreach(var mUser in matchUsers)
            {
                listUsers.Add(mUser.Login);
            }
            string [] result;
            if (listUsers.Count == 0) result = new string[] { };
            else
            {
                result = new string[listUsers.Count];
                for(i=0; i<listUsers.Count; i++) result[i] = listUsers[i];
            }

            return result;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            int i = 0;
            while (i < RoleKeysNames.keys.Length && RoleKeysNames.names[i] != roleName) i++;
            return (i < RoleKeysNames.keys.Length);
            
        }
    }
}