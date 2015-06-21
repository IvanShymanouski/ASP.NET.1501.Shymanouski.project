using System;
using System.Collections.Generic;

namespace ORM
{
    public class User : IORMHasIdEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RoleUser> Roles { get; set; }
        public virtual ICollection<TaskUser> Tasks { get; set; }
        public User()
        {
            Roles = new HashSet<RoleUser>();
            Tasks = new HashSet<TaskUser>();
        }
    }
}
