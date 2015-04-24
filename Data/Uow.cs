using System;
using Data.Repository;
using Db;
using Db.DbModel;

namespace Data
{
    public class Uow : IUow
    {
        public IPmsContext DbContext { get; set; }
        public IRepository<User> LegacyUserRepo { get { return new Repository<User>(DbContext); } }
        public IDataRepository<User> UserRepo { get { return new DataRepository<User>(DbContext); } }
        public Uow(IPmsContext dbContext)
        {
            if (dbContext == null)
            {
                throw new NullReferenceException("dbContext is null at Uow");
            }

            DbContext = dbContext;
            //DbContext.Configuration.LazyLoadingEnabled = true;
        }

        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
