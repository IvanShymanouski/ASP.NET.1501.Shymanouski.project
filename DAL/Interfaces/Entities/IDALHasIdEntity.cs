using System;

namespace DAL.Interfaces
{
    public interface IDALHasIdEntity: IDALEntity
    {
        Guid Id { get; set; }
    }
}
