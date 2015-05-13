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
        TEntity Find(Func<TEntity, bool> f); 

        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);

    }
}
