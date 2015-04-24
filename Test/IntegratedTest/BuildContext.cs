using System.Data.Entity;
using System.Linq;
using Db;
using NUnit.Framework;

namespace Test.IntegratedTest
{
    class BuildContext : PmsContext, IBuildDbContext
    {
        public BuildContext()
            : base(nameOrConnectionString: "DbPms")
        {
        }

        static BuildContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BuildContext>());
        }

        public void Load()
        {
            var users = Users.ToList();
        }
    }
}
