using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IHasIdService<TEntity> : IService<TEntity>
           where TEntity : class, IBLLHasIdEntity
    {
        TEntity GetById(Guid Id);
    }
}