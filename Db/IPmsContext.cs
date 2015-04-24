using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Db.DbModel;

namespace Db
{
    public interface IPmsContext : IDisposable
    {
        IDbSet<User> Users { get; set; }

        Database Database { get; }
        IPmsContextConfiguration Configuration { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IPmsDbEntityEntry<TEntity> EntryToAdd<TEntity>(TEntity entity) where TEntity : class;
        IPmsDbEntityEntry<TEntity> EntryToReplace<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
