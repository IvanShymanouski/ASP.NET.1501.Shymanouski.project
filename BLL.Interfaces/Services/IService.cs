using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace BLL.Interfaces
{
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetSingle(int key);
        IEnumerable<TEntity> Find(params Expression<Func<TEntity, bool>>[] predicates);

        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);

    }
}
