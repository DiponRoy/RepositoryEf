using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Db.Configuration;
using Db.DbModel;

namespace Db
{
    public class PmsContext : DbContext, IPmsContext
    {
        public IDbSet<User> Users { get; set; }

        public new IPmsContextConfiguration Configuration
        {
            get
            {
                return new PmsContextConfiguration(base.Configuration);
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public IPmsDbEntityEntry<TEntity> EntryToAdd<TEntity>(TEntity entity) where TEntity : class
        {
            return new PmsDbEntityEntry<TEntity>(Entry(entity));
        }

        public IPmsDbEntityEntry<TEntity> EntryToReplace<TEntity>(TEntity entity) where TEntity : class
        {
            return new PmsDbEntityEntry<TEntity>(Entry(entity));
        }

        protected PmsContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            if (String.IsNullOrEmpty(nameOrConnectionString))
            {
                throw new NullReferenceException("nameOrConnectionString is null or empty at PmsContext");
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfig());
        }
    }
}
