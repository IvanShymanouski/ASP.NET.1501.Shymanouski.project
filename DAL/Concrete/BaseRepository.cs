using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DAL.Interfaces;
using EntityBase;
using AutoMapper;

namespace DAL
{
    public class BaseRepository<TDalEntity, TModelEntity/*, TMapperModel*/> : IRepository<TDalEntity>
        // where TMapperModel : IMapperModel<TDalEntity, TModelEntity>, new()
        where TDalEntity : class, IEntity, new()
        where TModelEntity : class, IEntity, new()
    {
        //protected IMapperModel<TDalEntity, TModelEntity> _mapper = Mapper.CreateMap<TDalEntity, TModelEntity>();

        private readonly DbContext context;

        public BaseRepository(IUnitOfWork uow)
        {
            Mapper.CreateMap<TDalEntity, TModelEntity>();
            Mapper.CreateMap<TModelEntity, TDalEntity>();
            this.context = uow.Context;
        }

        public IEnumerable<TDalEntity> GetAll()
        {
            return context.Set<TModelEntity>().Select(Mapper.Map<TModelEntity, TDalEntity>);
        }

        public TDalEntity GetByID(int key)
        {
            TModelEntity model = context.Set<TModelEntity>().FirstOrDefault(e => e.Id == key);
            return Mapper.Map<TModelEntity, TDalEntity>(model);
        }

        public IEnumerable<TDalEntity> GetByPredicate(System.Linq.Expressions.Expression<Func<TDalEntity, bool>>[] f)
        {
            IQueryable<TModelEntity> temp = context.Set<TModelEntity>().AsQueryable();
            IQueryable<TDalEntity> tempDal = temp.Select(Mapper.Map<TModelEntity, TDalEntity>).AsQueryable();
            tempDal = f.Aggregate(tempDal, (current, predicate) => current.Where(predicate));
            return tempDal.AsEnumerable();
        }

        public void Create(TDalEntity entity)
        {
            TModelEntity modelEntity = Mapper.Map<TDalEntity, TModelEntity>(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            context.Set<TModelEntity>().Add(dbEntity.Entity);
        }

        public void Delete(TDalEntity entity)
        {
            TModelEntity modelEntity = Mapper.Map<TDalEntity, TModelEntity>(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            dbEntity.State = EntityState.Deleted;
        }

        public void Update(TDalEntity entity)
        {
            TModelEntity modelEntity = Mapper.Map<TDalEntity, TModelEntity>(entity);
            DbEntityEntry<TModelEntity> dbEntity = context.Entry<TModelEntity>(modelEntity);
            dbEntity.State = EntityState.Modified;
        }
    }
}