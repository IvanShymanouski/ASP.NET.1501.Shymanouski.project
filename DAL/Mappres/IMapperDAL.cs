using DAL.Interfaces;
using ORM;

namespace DAL
{
    public interface IMapperDAL<TORMEntity, TDALEntity>
        where TORMEntity : IORMEntity
        where TDALEntity : IDALEntity
    {
        TORMEntity ToORM(TDALEntity DALEntity);
        TDALEntity ToDAL(TORMEntity ormEntity);
    }
}
