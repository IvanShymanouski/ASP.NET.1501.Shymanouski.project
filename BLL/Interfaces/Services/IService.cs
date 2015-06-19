using System;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IService<TEntity> where TEntity : class, IBLLEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(Func<TEntity, bool> f);

        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
    }
}