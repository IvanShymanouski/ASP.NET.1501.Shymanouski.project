using System.Data.Entity.ModelConfiguration;


namespace ORM
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(r => r.Name).IsRequired();
        }
    }
}
