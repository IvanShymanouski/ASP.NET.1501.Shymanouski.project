using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class RoleUserService : BaseService<RoleUserDAL, 
                                               RoleUserEntity, 
                                               IRepository<RoleUserDAL>,
                                               RoleUserMapper
                                              >, IRoleUserService
    {
        public RoleUserService(IRoleUserRepository repository, IUnitOfWork uow) : base(repository, uow) { }

        public IEnumerable<RoleUserEntity> GetByUserId(Guid userId)
        {
            return ((IRoleUserRepository)_repository).GetByUserId(userId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }

        public IEnumerable<RoleUserEntity> GetByRoleId(Guid roleId)
        {
            return ((IRoleUserRepository)_repository).GetByRoleId(roleId).Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }
    }
}
