using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class RoleUserEntityConfiguration : EntityTypeConfiguration<RoleUser>
    {
        public RoleUserEntityConfiguration()
        {
            this.HasKey(u => new { u.UserId, u.RoleId });
        }
    }
}