using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;


namespace ORM
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {          
            this.HasKey<int>(r => r.Id);

            this.Property(r => r.Name).IsRequired();

            this.Property(r => r.Discription).IsOptional();
        }
    }
}
