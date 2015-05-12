using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntityBase;

namespace ORM
{
    [Table("Profile")]
    public class Profile : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public int? TaskId { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
