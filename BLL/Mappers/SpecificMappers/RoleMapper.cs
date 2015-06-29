using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class RoleMapper : IMapper<RoleDAL, RoleEntity>
    {
        public RoleDAL ToDAL(RoleEntity role)
        {
            return new RoleDAL()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public RoleEntity ToBLL(RoleDAL role)
        {
            return (null == role) ? null :
                new RoleEntity()
                {
                    Id = role.Id,
                    Name = role.Name
                };
        }
    }
}
