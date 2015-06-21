using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class UserMapper : IMapper<UserDAL, UserEntity>
    {
        public UserDAL ToDAL(UserEntity user)
        {
            return new UserDAL()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password
            };
        }

        public UserEntity ToBLL(UserDAL user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
