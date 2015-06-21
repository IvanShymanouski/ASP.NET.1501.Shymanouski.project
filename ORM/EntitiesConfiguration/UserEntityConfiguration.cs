using System.Data.Entity.ModelConfiguration;

namespace ORM
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Login).IsRequired();

            this.Property(u => u.Email).IsRequired();

            this.Property(u => u.Password).IsRequired();
        }
    }
}