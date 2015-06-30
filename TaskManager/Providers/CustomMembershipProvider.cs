using System;
using System.Web.Helpers;
using BLL.Interfaces;

namespace TaskManager.Providers
{
    public static class CustomMembershipProvider
    {
        public static UserEntity ValidateUserAndReturn(string emailOrLogin, string password)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            
            return users.Find(x => (x.Email == emailOrLogin || x.Login == emailOrLogin) && Crypto.VerifyHashedPassword(x.Password, password));
        }

        /// <summary>
        /// create new user
        /// </summary>
        /// <returns>null if user exist</returns>
        public static UserEntity CreateUser(string login, string email, string password)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            UserEntity membershipUser = null;

            if (null == FindUser(login, email))
            {
                UserEntity user = new UserEntity()
                            {
                                Id = Guid.NewGuid(),
                                Login = login,
                                Email = email,
                                Password = Crypto.HashPassword(password)
                            };
                users.Add(user);
                membershipUser = user;
            }

            return membershipUser;
        }

        private static UserEntity FindUser(string login, string email)
        {
            IHasIdService<UserEntity> users = (IHasIdService<UserEntity>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IHasIdService<UserEntity>));
            return users.Find(x => x.Email == email || x.Login == login);
        }
    }
}