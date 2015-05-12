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

        IEnumerable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>>[] f);

        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);


    }
}
