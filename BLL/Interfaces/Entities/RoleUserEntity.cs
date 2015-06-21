using System;

namespace BLL.Interfaces
{
    public class RoleUserEntity : IBLLEntity
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
