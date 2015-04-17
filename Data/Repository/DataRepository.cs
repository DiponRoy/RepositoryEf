using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Db;
using Db.DbModel.Interface;

namespace Data.Repository
{
    public class DataRepository<T> : Repository<T>, IDataRepository<T> where T : class, IDbEntity
    {
        public Expression Expression { get; private set; }
        public Type ElementType { get; private set; }
        public IQueryProvider Provider { get; private set; }


        public DataRepository(IPmsContext dbContext) : base(dbContext)
        {
            if (dbContext == null)
            {
                throw new Exception("dbContext null at DataRepository");
            }
            Expression = DbSet.Expression;
            ElementType = DbSet.ElementType;
            Provider = DbSet.Provider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DbSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
