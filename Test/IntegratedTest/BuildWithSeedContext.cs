using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Db;
using Db.DbModel;
using Db.DbModel.Enum;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Test.IntegratedTest
{
    class BuildWithSeedContext : PmsContext, IBuildDbContext
    {
        public BuildWithSeedContext()
            : base(nameOrConnectionString: "DbPms")
        {
        }

        static BuildWithSeedContext()
        {
            Database.SetInitializer(new BuildWithSeedContextInitializer());
        }

        public void Load()
        {
            Users.ToList();
        }
    }

    internal class BuildWithSeedContextInitializer : DropCreateDatabaseAlways<BuildWithSeedContext>
    {
        public BuildWithSeedContextInitializer()
        {
        }

        protected override void Seed(BuildWithSeedContext context)
        {
            List<User> users = Builder<User>.CreateListOfSize(10).All()
                .With(x => x.AddedBy, 1).With(x => x.Id, 0).Build().ToList();
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
