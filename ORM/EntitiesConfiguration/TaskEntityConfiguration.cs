using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class TaskEntityConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskEntityConfiguration()
        {
            this.HasKey<int>(t => t.Id);

        }
    }
}
