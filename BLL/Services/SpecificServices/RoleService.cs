using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Interfaces;

namespace BLL
{
    public class RoleService : BaseService<RoleEntity, RoleDal, IRoleRepository, RoleMapper>, IRoleService
    {
        public RoleService(IRoleRepository repository, IUnitOfWork uow) : base(repository, uow) { }
    }
}
