using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;

namespace Data
{
    public interface IUow : IDisposable
    {
        IPmsContext DbContext { get; set; }
        int Commit();
    }
}
