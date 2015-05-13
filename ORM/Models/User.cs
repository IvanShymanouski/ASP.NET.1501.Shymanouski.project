using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntityBase;

namespace ORM
{
    [Table("User")]
    public partial class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } 


        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public User()
        {
            Tasks = new List<Task>();
        }
    }
}
