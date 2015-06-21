using System.Data.Entity;
using System.Diagnostics;

namespace ORM
{
    public class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModel")
        {
            Debug.WriteLine("Context create!");
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TaskUser> TaskUser { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleEntityConfiguration());
            modelBuilder.Configurations.Add(new RoleUserEntityConfiguration());
            modelBuilder.Configurations.Add(new TaskEntityConfiguration());
            modelBuilder.Configurations.Add(new TaskUserEntityConfiguration());
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            
        }

        public new void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("Dispose in context!");
        }
    }
}
