using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class RoleUserMapper : IMapper<RoleUserDAL, RoleUserEntity>
    {
        public RoleUserDAL ToDAL(RoleUserEntity roleUser)
        {
            return new RoleUserDAL()
            {
                UserId = roleUser.UserId,
                RoleId = roleUser.RoleId
            };
        }

        public RoleUserEntity ToBLL(RoleUserDAL roleUser)
        {
            return (null == roleUser) ? null :
                new RoleUserEntity()
                {
                    UserId = roleUser.UserId,
                    RoleId = roleUser.RoleId
                };
        }
    }
}
