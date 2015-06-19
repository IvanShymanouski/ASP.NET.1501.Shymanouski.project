using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IDALEntity
    {
        IEnumerable<TEntity> GetAll();

        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
