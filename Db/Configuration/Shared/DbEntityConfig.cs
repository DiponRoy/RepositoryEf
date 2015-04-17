using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Db.DbModel.Interface;

namespace Todo.Db.Configurations.Shared
{
    public class DbEntityConfig<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class, IDbEntity
    {
        protected DbEntityConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Status)
                .IsRequired();

            Property(x => x.AddedBy)
                .IsRequired();

            Property(x => x.AddedDateTime)
                .IsRequired()
                .HasColumnType("DATETIME");

            Property(x => x.UpdatedDateTime)
                .HasColumnType("DATETIME");

            HasRequired(x => x.AddedByUser)
                .WithMany()
                .HasForeignKey(f => f.AddedBy)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.UpdatedByUser)
                .WithMany()
                .HasForeignKey(f => f.UpdatedBy)
                .WillCascadeOnDelete(false);

        }
    }
}