using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DAL.Interfaces;
using EntityBase; 

namespace DAL
{
    public class BaseRepository<TDalEntity, TModelEntity, TEntityMapper> : IRepository<TDalEntity> 
        where TDalEntity : class, IEntity, new()
        where TModelEntity : class, IEntity, new()
        where TEntityMapper : IMapper<TModelEntity, TDalEntity>, new()
    {
        private readonly DbContext context;
        protected IMapper<TModelEntity, TDalEntity> _entityMapper = new TEntityMapper();

        public BaseRepository(IUnitOfWork uow)
        { 
            this.context = uow.Context;
        }

        public IEnumerable<TDalEntity> GetAll()
        {
            Func<TModelEntity, TDalEntity> f = (obj) => _entityMapper.ToDal(obj);
            return context.Set<TModelEntity>().Select(f);
        }

        public TDalEntity GetByID(int key)
        {
            TModelEntity model = context.Set<TModelEntity>().FirstOrDefault(e => e.Id == key);
            return _entityMapper.ToDal(model);
        } 

        public void Create(TDalEntity entity)
        {
            TModelEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            context.Set<TModelEntity>().Add(dbEntity.Entity);
        }

        public void Delete(TDalEntity entity)
        {
            TModelEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            dbEntity.State = EntityState.Deleted;
        }

        public void Update(TDalEntity entity)
        {
            TModelEntity modelEntity = _entityMapper.ToORM(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            dbEntity.State = EntityState.Modified;
        }
    }
}