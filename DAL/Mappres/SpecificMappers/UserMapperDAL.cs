using System;
using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class UserMapperDAL : IMapperDAL<User, UserDAL>
    {
        public User ToORM(UserDAL userDAL)
        {
            return new User()
            {
                Id = userDAL.Id,
                Login = userDAL.Login,
                Email = userDAL.Email,
                Password = userDAL.Password,
                RoleId = userDAL.RoleId
            };
        }

        public UserDAL ToDAL(User user)
        {
            return new UserDAL()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }
    }
}
