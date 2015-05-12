using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntityBase;

namespace ORM
{
    [Table("Task")]
    public class Task : IEntity
    {


        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }

        public int UserId { get; set; }
        public virtual ICollection<Profile> Users { get; set; }
    }

}
