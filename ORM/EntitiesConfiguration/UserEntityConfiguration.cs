using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            // PK
            this.HasKey<int>(u => u.Id);

            this.Property(u => u.Email).IsRequired();

            this.Property(u => u.Password).IsRequired(); 

        }
    }
}