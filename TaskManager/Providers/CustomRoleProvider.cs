using System;
using System.Collections.Generic;
using System.Web.Security;
using BLL.Interfaces;
using TaskManager.Infrastructure;

namespace TaskManager.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string emailOrLogin)
        {
            string[] userRoles = new string[] { };

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));            

            UserEntity user = users.Find(x => x.Email == emailOrLogin || x.Login == emailOrLogin);
            
            if (null != user)
            {
                IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));
                IEnumerable<RoleUserEntity> roles = roleUsers.GetByUserId(user.Id);

                List<string> ur = new List<string>();
                foreach (var role in roles)
                {
                    int ind = 0;
                    while (ind < RoleKeysNames.keys.Length && RoleKeysNames.keys[ind] != role.RoleId) ind++;
                    if (ind == RoleKeysNames.keys.Length) throw new ArgumentException("User " + user.Id + " has invalid roleId :" + role.RoleId);
                    ur.Add(RoleKeysNames.names[ind]);
                }
                userRoles = new string[ur.Count];
                for (int i = 0; i < ur.Count; i++) userRoles[i] = ur[i];
            }
            return userRoles;
        }

        public override bool IsUserInRole(string emailOrLogin, string roleName)
        {
            bool inRole = false;

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));

            UserEntity user = users.Find(x => x.Email == emailOrLogin || x.Login == emailOrLogin);            

            if (null != user)
            {
                IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));
                IEnumerable<RoleUserEntity> roles = roleUsers.GetByUserId(user.Id);

                Guid roleId = GetRoleId(roleName);

                foreach (var role in roles)
                {
                    if (role.RoleId == roleId)
                    {
                        inRole = true;
                        break;
                    }
                }
            }
            return inRole;
        }

        public override string[] GetAllRoles()
        {
            return RoleKeysNames.names;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string[] result = new string[] { };

            Guid roleId = GetRoleId(roleName);

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));

            IEnumerable<RoleUserEntity> usersInRole = roleUsers.GetByRoleId(roleId);
            
            List<string> listUsers = new List<string>();
            foreach(var mUser in usersInRole)
            {
                listUsers.Add(users.GetById(mUser.UserId).Login);
            }
            
            if (listUsers.Count != 0)            
            {
                result = new string[listUsers.Count];
                for(int i=0; i<listUsers.Count; i++) result[i] = listUsers[i];
            }

            return result;
        }

        public Guid[] GetUsersIdInRole(string roleName)
        {
            Guid[] result = new Guid[] { };

            Guid roleId = GetRoleId(roleName);

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));

            IEnumerable<RoleUserEntity> usersInRole = roleUsers.GetByRoleId(roleId);

            List<Guid> listUsers = new List<Guid>();
            foreach (var mUser in usersInRole)
            {
                listUsers.Add(mUser.UserId);
            }

            if (listUsers.Count != 0)
            {
                result = new Guid[listUsers.Count];
                for (int i = 0; i < listUsers.Count; i++) result[i] = listUsers[i];
            }

            return result;
        }

        public override bool RoleExists(string roleName)
        {
            int i = 0;
            while (i < RoleKeysNames.keys.Length && RoleKeysNames.names[i] != roleName) i++;
            return (i < RoleKeysNames.keys.Length);            
        }

        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));
            foreach (var roleName in roleNames)
            {
                Guid roleId = GetRoleId(roleName);

                foreach (var userName in userNames)
                {
                    UserEntity user = users.Find(x => x.Login == userName || x.Email == userName);
                    if (null == roleUsers.Find( x => x.RoleId == roleId && user.Id == x.UserId))
                        roleUsers.Add(new RoleUserEntity { UserId = user.Id, RoleId = roleId });
                }
            }            
        }

        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));
            foreach (var roleName in roleNames)
            {
                Guid roleId = GetRoleId(roleName);

                foreach (var userName in userNames)
                {
                    UserEntity user = users.Find(x => x.Login == userName || x.Email == userName);
                    if (null != roleUsers.Find(x => x.RoleId == roleId && user.Id == x.UserId))
                        roleUsers.Delete(new RoleUserEntity { UserId = user.Id, RoleId = roleId });
                }
            }            
        }

        #region NotImplemented
        public override void CreateRole(string roleName)
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
        #endregion

        public Guid GetRoleId(string roleName)
        {
            int ind = 0;
            while (ind < RoleKeysNames.names.Length && RoleKeysNames.names[ind] != roleName) ind++;
            if (ind == RoleKeysNames.names.Length) throw new ArgumentException("Enter invalid roleName :" + roleName);

            return RoleKeysNames.keys[ind];
        }
    }
}