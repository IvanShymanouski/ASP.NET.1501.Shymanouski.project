using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class TaskUserRelationEntityConfiguration : EntityTypeConfiguration<TaskUserRelation>
    {
        public TaskUserRelationEntityConfiguration()
        {
            this.HasKey(u => new { u.UserId, u.TaskId });

            this.Property(u => u.Progress).IsRequired();

            this.Property(u => u.Status).IsRequired(); 
        }
    }
}