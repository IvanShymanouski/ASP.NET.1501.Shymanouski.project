using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace BLL
{
    public interface IMapper<TBllEntity, TDalEntity>
        where TBllEntity : IEntity
        where TDalEntity : IEntity
    {
        TBllEntity ToBll(TDalEntity dalEntity);
        TDalEntity ToDal(TBllEntity bllEntity);
    }
}
