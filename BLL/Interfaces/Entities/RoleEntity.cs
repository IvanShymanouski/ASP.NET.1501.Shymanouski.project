using System;

namespace BLL.Interfaces
{
    public class RoleEntity : IBLLHasIdEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}