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
        DbContextConfiguration Configuration { get; }

        IDbSet<TEntity> EntitySet<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
