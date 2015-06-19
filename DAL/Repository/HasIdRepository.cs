using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public class HasIdRepository<TEntity, TDALEntity, TEntityMapper> : BaseRepository<TEntity, TDALEntity, TEntityMapper>, IHasIdRepository<TDALEntity>
        where TEntity : class, IORMHasIdEntity, new()
        where TDALEntity : class, IDALHasIdEntity, new()
        where TEntityMapper : IMapperDAL<TEntity, TDALEntity>, new()
    {
        public HasIdRepository(DbContext context) : base(context) { }

        public TDALEntity GetById(Guid Id)
        {
            Func<TEntity, TDALEntity> f = (obj) => _entityMapper.ToDAL(obj);
            return _context.Set<TEntity>().AsNoTracking().Where(x => x.Id == Id).Select(f).FirstOrDefault();
        } 

    }
}