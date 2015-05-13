using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class RoleRepository : BaseRepository<RoleDal, Role, RoleMapper>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork uow) : base(uow) { }

    }
}