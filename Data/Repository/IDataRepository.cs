using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IDataRepository<T> : IRepository<T>, IQueryable<T>
    {
    }
}
