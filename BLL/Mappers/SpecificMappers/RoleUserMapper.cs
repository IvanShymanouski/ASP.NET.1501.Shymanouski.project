using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class RoleUserMapper : IMapper<RoleUserDAL, RoleUserEntity>
    {
        public RoleUserDAL ToDAL(RoleUserEntity toleUser)
        {
            return new RoleUserDAL()
            {
                UserId = toleUser.UserId,
                RoleId = toleUser.RoleId
            };
        }

        public RoleUserEntity ToBLL(RoleUserDAL toleUser)
        {
            return new RoleUserEntity()
            {
                UserId = toleUser.UserId,
                RoleId = toleUser.RoleId
            };
        }
    }
}
