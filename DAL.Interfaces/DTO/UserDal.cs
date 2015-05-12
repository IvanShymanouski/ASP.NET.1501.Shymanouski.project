using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityBase;

namespace DAL.Interfaces
{
    public class UserDal : IEntity
    {

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }

        public int RoleId { get; set; }
    }
}