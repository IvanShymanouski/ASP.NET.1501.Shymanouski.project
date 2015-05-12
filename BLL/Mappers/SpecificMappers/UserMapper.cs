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
        public UserEntity ToBll(UserDal dalEntity)
        {
            return dalEntity.ToBll();
        }

        public UserDal ToDal(UserEntity bllEntity)
        {
            return bllEntity.ToDalFromBll();
        }
    }
}
