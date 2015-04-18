using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repository;
using Db;
using Db.DbModel;

namespace Data
{
    public interface IUow : IDisposable
    {
        IPmsContext DbContext { get; set; }
        IRepository<User> LegacyUserRepo { get; }
        IDataRepository<User> UserRepo { get; }
        int Commit();
    }
}
