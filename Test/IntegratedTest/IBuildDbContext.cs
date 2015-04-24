using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Db;
using NUnit.Framework;

namespace Test.IntegratedTest
{
    public interface IBuildDbContext : IPmsContext
    {
        void Load();
    }
}
