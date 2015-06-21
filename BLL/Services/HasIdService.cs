using System;
using DAL.Interfaces;
using BLL.Interfaces; 

namespace BLL
{
    public class HasIdService<TDto, TEntity, TRepository, TEntityMapper> : BaseService<TDto, TEntity, TRepository, TEntityMapper>, IHasIdService<TEntity>
        where TDto : class, IDALHasIdEntity
        where TEntity : class, IBLLHasIdEntity
        where TRepository : IHasIdRepository<TDto>
        where TEntityMapper : IMapper<TDto, TEntity>, new()
    {
        public HasIdService(TRepository repository, IUnitOfWork uow) : base(repository, uow) { }

        public TEntity GetById(Guid key)
        {
            return _entityMapper.ToBLL(_repository.GetById(key));
        }
    }
}