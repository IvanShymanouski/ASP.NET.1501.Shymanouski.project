using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using EntityBase;
using DAL.Interfaces;
using BLL.Interfaces;
using AutoMapper;

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
            Mapper.CreateMap<TDto, TEntity>();
            Mapper.CreateMap<TEntity, TDto>();
            this._repository = repository;
            this._uow = uow;
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

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll().Select(GetEntity);
        }

        public virtual TEntity GetSingle(int key)
        {
            TDto dto = _repository.GetByID(key);
            return GetEntity(dto);
        }

        public virtual IEnumerable<TEntity> Find(params System.Linq.Expressions.Expression<Func<TEntity, bool>>[] predicates)
        {

            Expression<Func<TDto, bool>>[] predicatesDto = new Expression<Func<TDto, bool>>[predicates.Length];
            int i = 0;
            foreach (var p in predicates)
            {
                predicatesDto[i++] = ChangeInputType<TEntity, TDto, bool>(p);
            }
            return _repository.GetByPredicate(predicatesDto).Select(GetEntity);
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

        private Expression<Func<TDto, bool>> ChangeInputType<TEntity, TDto, TResult>(Expression<Func<TEntity, bool>> expression)
        {
            //if (!typeof(TEntity).IsAssignableFrom(typeof(TDto)))
            //    throw new Exception(string.Format("{0} is not assignable from {1}.", typeof(TEntity), typeof(TDto)));
            var beforeParameter = expression.Parameters.Single();
            var afterParameter = Expression.Parameter(typeof(TDto), beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);
            return Expression.Lambda<Func<TDto, bool>>(visitor.Visit(expression.Body), afterParameter);
        }
    }

    public class SubstitutionExpressionVisitor : ExpressionVisitor
    {
        private Expression before, after;
        public SubstitutionExpressionVisitor(Expression before, Expression after)
        {
            this.before = before;
            this.after = after;
        }
        public override Expression Visit(Expression node)
        {
            return node == before ? after : base.Visit(node);
        }
    }
}