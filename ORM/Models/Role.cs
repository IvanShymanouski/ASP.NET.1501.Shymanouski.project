using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using EntityBase;

namespace ORM
{
    [Table("Role")]
    public partial class Role : IEntity
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
