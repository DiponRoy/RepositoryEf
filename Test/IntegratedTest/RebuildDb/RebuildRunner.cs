using System.Linq;
using Db;
using NUnit.Framework;

namespace Test.IntegratedTest.RebuildDb
{
    [TestFixture]
    public class RebuildRunner
    {
        protected IPmsContext Db { get; set; }

        [Test]
        public void RebuildSchema()
        {
            Db = new BuildContext();
            Assert.DoesNotThrow(LoadDb);
        }

        [Test]
        public void RebuildSchemaWithData()
        {
            Db = new BuildWithSeedContext();
            Assert.DoesNotThrow(LoadDb);
        }

        private void LoadDb()
        {
            using (Db)
            {
                Db.Users.ToList();             
            }
        }

    }
}
