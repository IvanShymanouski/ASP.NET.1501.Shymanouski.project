using System;

namespace BLL.Interfaces
{
    public interface IBLLHasIdEntity : IBLLEntity
    {
        Guid Id { get; set; }
    }
}