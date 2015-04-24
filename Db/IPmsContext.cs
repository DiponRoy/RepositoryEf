using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db.DbModel;

namespace Db
{
    public interface IPmsContext : IDisposable
    {
        IDbSet<User> Users { get; set; }


        DbContextConfiguration Configuration { get; }
        IDbSet<TEntity> EntitySet<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
