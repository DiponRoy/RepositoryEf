using System;
using System.Data.Entity;
using Db;
using Db.DbModel;
using Db.DbModel.Enum;

namespace Test.IntegratedTest
{
    class BuildWithSeedContext : PmsContext
    {
        public BuildWithSeedContext()
            : base(nameOrConnectionString: "DbPms")
        {
        }

        static BuildWithSeedContext()
        {
            Database.SetInitializer(new BuildWithSeedContextInitializer());
        }
    }

    internal class BuildWithSeedContextInitializer : DropCreateDatabaseAlways<BuildWithSeedContext>
    {
        public BuildWithSeedContextInitializer()
        {
        }

        protected override void Seed(BuildWithSeedContext context)
        {
            context.Users.Add(
                new User()
                {
                    Name = "Admin",
                    Email = "Admin@gmail.com",
                    Password = "Admin",
                    Status = EntityStatusEnum.Active,
                    AddedBy = 1,
                    AddedDateTime = DateTime.Now
                });
            context.SaveChanges();
        }
    }
}
