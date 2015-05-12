using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace DAL
{
    public interface IMapperModel<TDalEntity, TModelEntity>
        where TDalEntity : IEntity
        where TModelEntity : IEntity
    {
        TModelEntity ToModel(TDalEntity dalEntity);
        TDalEntity ToDal(TModelEntity bllEntity);
    }
}
