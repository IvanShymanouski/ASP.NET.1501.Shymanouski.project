using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class RoleMapper : IMapper<RoleEntity, RoleDal>
    {
        public RoleEntity ToBll(RoleDal dalEntity)
        {
            return dalEntity.ToBll();
        }

        public RoleDal ToDal(RoleEntity bllEntity)
        {
            return bllEntity.ToDalFromBll();
        }
    }
}
