using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class RoleUserMapperDAL : IMapperDAL<RoleUser, RoleUserDAL>
    {
        public RoleUser ToORM(RoleUserDAL RoleUserRelationDAL)
        {
            return new RoleUser()
            {
                UserId = RoleUserRelationDAL.UserId,
                RoleId = RoleUserRelationDAL.RoleId
            };
        }

        public RoleUserDAL ToDAL(RoleUser RoleUserRelation)
        {
            return new RoleUserDAL()
            {
                UserId = RoleUserRelation.UserId,
                RoleId = RoleUserRelation.RoleId
            };
        }
    }
}
