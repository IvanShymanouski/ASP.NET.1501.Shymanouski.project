using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class UserMapper : IMapper<UserEntity, UserDal>
    {
        public UserEntity ToBll(UserDal user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }

        public UserDal ToDal(UserEntity user)
        {
            return new UserDal()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };
        }
    }
}
