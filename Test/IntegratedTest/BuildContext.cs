using System.Data.Entity;
using Db;

namespace Test.IntegratedTest
{
    class BuildContext : PmsContext
    {
        public BuildContext()
            : base(nameOrConnectionString: "DbPms")
        {
        }

        static BuildContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BuildContext>());
        }
    }
}
