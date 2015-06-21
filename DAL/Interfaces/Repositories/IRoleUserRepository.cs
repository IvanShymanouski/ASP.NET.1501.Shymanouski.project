using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRoleUserRepository : IRepository<RoleUserDAL>
    {
        IEnumerable<RoleUserDAL> GetByUserId(Guid userId);
        IEnumerable<RoleUserDAL> GetByRoleId(Guid taskId);
    }
}
