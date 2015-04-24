using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db
{
    public interface IPmsDbEntityEntry<T>
    {
        EntityState State { get; set; }
    }
}
