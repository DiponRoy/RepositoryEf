using System.Linq;
using Db;
using NUnit.Framework;

namespace Test.IntegratedTest.RebuildDb
{
    [TestFixture]
    public class RebuildRunner
    {
        protected IBuildDbContext Db { get; set; }

        [Test]
        public void RebuildSchema()
        {
            Db = new BuildContext();
            Assert.DoesNotThrow(() => Db.Load());
        }

        [Test]
        public void RebuildSchemaWithData()
        {
            Db = new BuildWithSeedContext();
            Assert.DoesNotThrow(() => Db.Load());
        }
    }
}
