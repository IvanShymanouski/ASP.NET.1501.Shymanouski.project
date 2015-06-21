using System;

namespace DAL.Interfaces
{
    public interface IHasIdRepository<TEntity> : IRepository<TEntity>
           where TEntity : IDALHasIdEntity
    {
        TEntity GetById(Guid Id);
    }
}
