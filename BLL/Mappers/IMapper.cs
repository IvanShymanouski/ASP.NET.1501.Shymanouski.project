using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public interface IMapper<TDALEntity, TBLLEntity>
        where TDALEntity : IDALEntity
        where TBLLEntity : IBLLEntity
    {
        TDALEntity ToDAL(TBLLEntity bllEntity);
        TBLLEntity ToBLL(TDALEntity dalEntity);
    }
}
