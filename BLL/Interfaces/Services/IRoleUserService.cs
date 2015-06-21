using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IRoleUserService : IService<RoleUserEntity>
    {
        IEnumerable<RoleUserEntity> GetByUserId(Guid userId);
        IEnumerable<RoleUserEntity> GetByRoleId(Guid roleId);
    }
}
