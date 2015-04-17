using System;
using Db;

namespace Data
{
    public class Uow : IUow
    {
        public IPmsContext DbContext { get; set; }

        public Uow(IPmsContext dbContext)
        {
            if (dbContext == null)
            {
                throw new NullReferenceException("dbContext is null at Uow");
            }

            DbContext = dbContext;
        }

        public Uow() : this(DbContextFactory.Get())
        {
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
