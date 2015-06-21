using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class RoleUserRepository : BaseRepository<RoleUser, RoleUserDAL, RoleUserMapperDAL>, IRoleUserRepository
    {
        public RoleUserRepository(DbContext context) : base(context) { }

        public IEnumerable<RoleUserDAL> GetByUserId(Guid userId)
        {
            Func<RoleUser, RoleUserDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<RoleUser>().AsNoTracking().Where(x => x.UserId == userId).Select(f);
        }

        public IEnumerable<RoleUserDAL> GetByRoleId(Guid roleId)
        {
            Func<RoleUser, RoleUserDAL> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<RoleUser>().AsNoTracking().Where(x => x.RoleId == roleId).Select(f);
        }
    }
}
