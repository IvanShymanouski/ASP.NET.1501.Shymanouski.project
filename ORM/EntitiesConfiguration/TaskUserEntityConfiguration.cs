using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class TaskUserEntityConfiguration : EntityTypeConfiguration<TaskUser>
    {
        public TaskUserEntityConfiguration()
        {
            this.HasKey(u => new { u.UserId, u.TaskId });

            this.Property(u => u.Progress).IsRequired();
        }
    }
}