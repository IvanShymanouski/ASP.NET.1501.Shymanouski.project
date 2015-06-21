using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interfaces;
using BLL.Interfaces; 

namespace BLL
{
    public abstract class BaseService<TDto, TEntity, TRepository, TEntityMapper> : IService<TEntity>
        where TDto : class, IDALEntity
        where TEntity : class, IBLLEntity
        where TRepository : IRepository<TDto>
        where TEntityMapper : IMapper<TDto, TEntity>, new()
    {
        public readonly TRepository _repository;
        protected readonly IUnitOfWork _uow;
        protected IMapper<TDto, TEntity> _entityMapper = new TEntityMapper();

        public BaseService(TRepository repository, IUnitOfWork uow)
        { 
            this._repository = repository;
            this._uow = uow;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll().Select(dalEntity => _entityMapper.ToBLL(dalEntity));
        }

        public virtual TEntity Find(Func<TEntity, bool> f)
        {
            return _repository.GetAll().Select(dalEntity => _entityMapper.ToBLL(dalEntity)).FirstOrDefault(f);
        }

        public virtual void Add(TEntity entity)
        {            
            _repository.Create(_entityMapper.ToDAL(entity));
            _uow.Commit();
        }

        public virtual void Edit(TEntity entity)
        {
            _repository.Update(_entityMapper.ToDAL(entity));
            _uow.Commit();
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(_entityMapper.ToDAL(entity));
            _uow.Commit();
        } 
    }
}