using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DAL.Interfaces;
using ORM;

namespace DAL
{
    public abstract class BaseRepository<TEntity, TDALEntity, TEntityMapper> : IRepository<TDALEntity>
        where TEntity : class, IORMEntity, new()
        where TDALEntity : class, IDALEntity, new()
        where TEntityMapper : IMapperDAL<TEntity, TDALEntity>, new()
    {
        protected readonly DbContext _context;
        protected IMapperDAL<TEntity, TDALEntity> _entityMapper = new TEntityMapper();

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<TDALEntity> GetAll()
        {
            Func<TEntity, TDALEntity> f = (obj) => _entityMapper.ToDAL(obj);            
           return _context.Set<TEntity>().AsNoTracking().Select(f);
        }

        public virtual void Create(TDALEntity entity)
        {
            TEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TEntity> dbEntity = _context.Entry<TEntity>(modelEntity);
            dbEntity.State = EntityState.Added;            
        }

        public virtual void Delete(TDALEntity entity)
        {
            TEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TEntity> dbEntity = _context.Entry<TEntity>(modelEntity);            
            dbEntity.State = EntityState.Deleted;
        }

        public virtual void Update(TDALEntity entity)
        {
            TEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TEntity> dbEntity = _context.Entry<TEntity>(modelEntity);
            dbEntity.State = EntityState.Modified;
        }
    }
}