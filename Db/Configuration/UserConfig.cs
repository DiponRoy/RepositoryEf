using Db.DbModel;
using Todo.Db.Configurations.Shared;

namespace Db.Configuration
{
    public class UserConfig : DbEntityConfig<User>
    {
        public UserConfig():base()
        {
            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            Property(x => x.ContactNo)
                .IsOptional()
                .HasMaxLength(20);

            Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}