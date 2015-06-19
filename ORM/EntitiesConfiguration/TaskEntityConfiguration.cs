using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class TaskEntityConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskEntityConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(t => t.Title).IsRequired();

            this.Property(t => t.Description).IsRequired();
        }
    }
}
