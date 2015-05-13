using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

using EntityBase;
using DAL.Interfaces;
using BLL.Interfaces; 

namespace BLL
{
    public abstract class BaseService<TEntity, TDto, TRepository, TEntityMapper> : IService<TEntity>
        where TEntity : class, IEntity
        where TDto : class, IEntity
        where TRepository : IRepository<TDto>
        where TEntityMapper : IMapper<TEntity, TDto>, new()
    {
        public readonly TRepository _repository;
        protected readonly IUnitOfWork _uow;
        protected IMapper<TEntity, TDto> _entityMapper = new TEntityMapper();

        public BaseService(TRepository repository, IUnitOfWork uow)
        { 
            this._repository = repository;
            this._uow = uow;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll().Select(GetEntity);
        }

        public virtual TEntity GetSingle(int key)
        {
            TDto dto = _repository.GetByID(key);
            return GetEntity(dto);
        }

        public virtual TEntity Find(Func<TEntity, bool> f)
        {
            return _repository.GetAll().Select(GetEntity).FirstOrDefault(f);
        }

        public virtual void Add(TEntity entity)
        {
            TDto dto = GetDto(entity);
            _repository.Create(dto);
            _uow.Commit();
        }

        public virtual void Edit(TEntity entity)
        {
            TDto dto = GetDto(entity);
            _repository.Update(dto);
            _uow.Commit();
        }

        public virtual void Delete(TEntity entity)
        {
            TDto dto = GetDto(entity);
            _repository.Delete(dto);
            _uow.Commit();
        }

        protected TEntity GetEntity(TDto dto)
        {
            var entity = _entityMapper.ToBll(dto);
            return entity;
        }

        protected TDto GetDto(TEntity entity)
        {
            var dto = _entityMapper.ToDal(entity);
            return dto;
        }
 
    }
}