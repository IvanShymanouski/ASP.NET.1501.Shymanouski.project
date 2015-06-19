using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IHasIdRepository<TEntity> : IRepository<TEntity>
           where TEntity : IDALHasIdEntity 
    {
        TEntity GetById(Guid Id);
    }
}
