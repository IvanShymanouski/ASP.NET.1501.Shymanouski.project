using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetByID(int key);

        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);


    }
}
