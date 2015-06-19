using System;

namespace ORM
{
    public interface IORMHasIdEntity : IORMEntity
    {
        Guid Id { get; set; }
    }
}
