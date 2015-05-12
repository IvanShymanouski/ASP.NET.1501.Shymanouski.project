using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Default name for all tables.
            // modelBuilder.HasDefaultSchema("Manager");

            // Set configuration files for tables of database.
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new RoleEntityConfiguration());
            modelBuilder.Configurations.Add(new TaskEntityConfiguration());

        }

        public new void Dispose()
        {
            base.Dispose();
            Debug.WriteLine("Dispose in context!");
        }
    }
}
