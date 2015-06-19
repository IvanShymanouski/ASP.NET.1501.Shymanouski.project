using System;
using ORM;
using DAL.Interfaces;

namespace DAL
{
    public class RoleMapperDAL : IMapperDAL<Role, RoleDAL>
    {
        public Role ToORM(RoleDAL roleDAL)
        {
            return new Role()
            {
                Id = roleDAL.Id,
                Name = roleDAL.Name
            };
        }

        public RoleDAL ToDAL(Role role)
        {
            return new RoleDAL()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
