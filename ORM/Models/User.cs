using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    public class User : IORMHasIdEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<TaskUserRelation> TaskUserRelation { get; set; }
        public User()
        {
            TaskUserRelation = new HashSet<TaskUserRelation>();
        }
    }
}
