using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Db;
using Db.DbModel.Enum;
using Db.DbModel.Interface;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IDbEntity
    {
        protected IPmsContext DbContext { get; set; }
        protected IDbSet<T> DbSet { get; set; }

        public Repository(IPmsContext dbContext)
        {
            if (dbContext == null)
            {
                throw new Exception("dbContext null at Repository<T>");
            }
            DbContext = dbContext;
            DbSet = DbContext.EntitySet<T>();
        }

        public virtual T Add(T entity)
        {
            if (entity.AddedBy == null)
            {
                throw new Exception("AddedBy credential require.");
            }
            if (entity.Status == null)
            {
                throw new Exception("Status require.");
            }
            entity.AddedDateTime = DateTime.UtcNow;

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }

            return entity;
        }

        public virtual T Replace(T entity)
        {
            if (entity.UpdatedBy == null)
            {
                throw new Exception("UpdatedBy credential require.");
            }
            if (entity.Status == null)
            {
                throw new Exception("Status require.");
            }
            entity.UpdatedDateTime = DateTime.UtcNow;

            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;

            return entity;
        }

        public virtual T Remove(T entity)
        {
            entity.Status = EntityStatusEnum.Removed;
            return Replace(entity);
        }

        public virtual IQueryable<T> All()
        {
            return DbSet;
        }
    }
}
