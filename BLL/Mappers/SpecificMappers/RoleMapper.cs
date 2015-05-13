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
        public RoleEntity ToBll(RoleDal role)
        {
            return new RoleEntity()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public RoleDal ToDal(RoleEntity role)
        {
            return new RoleDal()
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
