using System;

namespace BLL.Interfaces
{
    public class UserEntity : IBLLHasIdEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
    }
}
