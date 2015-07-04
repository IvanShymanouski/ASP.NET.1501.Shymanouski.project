using System;
using System.Collections.Generic;
using BLL.Interfaces;
using TaskManager.Infrastructure;
using TaskManager.Authentification;
using System.Security.Principal;

namespace TaskManager.Providers
{
    public static class CustomRoleProvider
    {
        public static string[] GetRolesForUser(string emailOrLogin)
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
                    while (ind < RoleKeys.keys.Count && RoleKeys.keys[ind] != role.RoleId) ind++;
                    if (ind == RoleKeys.keys.Count) throw new ArgumentException("User " + user.Id + " has invalid roleId :" + role.RoleId);
                    ur.Add(RoleKeys.names[ind]);
                }
                userRoles = new string[ur.Count];
                for (int i = 0; i < ur.Count; i++) userRoles[i] = ur[i];
            }
            return userRoles;
        }
        
        /// <returns>true if user have role from roles</returns>
        public static bool IsUserInRoles(IIdentity identity, IEnumerable<string> roleNames)
        {
            Identity user = identity as Identity;
            bool inRoles = false;

            if (null != user)
            {
                IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));

                foreach (var roleName in roleNames)
                {
                    Guid roleId = GetRoleId(roleName);
                    if (null != roleUsers.Find(x => x.RoleId == roleId && user.Id == x.UserId))
                    {
                        inRoles = true;
                        break;
                    }
                }
            }

            return inRoles;
        }

        public static string[] GetAllRoles()
        {
            return RoleKeys.names.ToArray();
        }

        public static string[] GetUsersInRole(string roleName)
        {
            string[] result = new string[] { };

            Guid roleId = GetRoleId(roleName);

            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));

            IEnumerable<RoleUserEntity> usersInRole = roleUsers.GetByRoleId(roleId);

            List<string> listUsers = new List<string>();
            foreach (var mUser in usersInRole)
            {
                listUsers.Add(users.GetById(mUser.UserId).Login);
            }

            if (listUsers.Count != 0)
            {
                result = new string[listUsers.Count];
                for (int i = 0; i < listUsers.Count; i++) result[i] = listUsers[i];
            }

            return result;
        }

        public static Guid[] GetUsersIdInRole(string roleName)
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

        public static void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            IRoleUserService roleUsers = (IRoleUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleUserService));
            foreach (var roleName in roleNames)
            {
                Guid roleId = GetRoleId(roleName);

                foreach (var userName in userNames)
                {
                    UserEntity user = users.Find(x => x.Login == userName || x.Email == userName);
                    if (null == roleUsers.Find(x => x.RoleId == roleId && user.Id == x.UserId))
                        roleUsers.Add(new RoleUserEntity { UserId = user.Id, RoleId = roleId });
                }
            }
        }

        public static void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
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

        public static Guid GetRoleId(string roleName)
        {
            int ind = 0;
            while (ind < RoleKeys.names.Count && RoleKeys.names[ind] != roleName) ind++;
            if (ind == RoleKeys.names.Count) throw new ArgumentException("Enter invalid roleName :" + roleName);

            return RoleKeys.keys[ind];
        }
    }
}