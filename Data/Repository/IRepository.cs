using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository<T> 
    {
        T Add(T model);
        T Replace(T model);
        T Remove(T model);
    }
}
