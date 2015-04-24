using System;
using System.Linq;
using Data;
using Db;
using Db.DbModel;
using Db.DbModel.Enum;
using FizzWare.NBuilder;
using NUnit.Framework;

namespace Test.IntegratedTest.Data
{
    [TestFixture]
    public class LegacyUserRepoTest
    {
        protected IPmsContext DbContext { get; set; }
        protected IUow Uow { get; set; }

        [SetUp]
        public void SetUp()
        {
            DbContext = new BuildContext();
            DbContext.Database.Initialize(true);
        }

        protected void InitializeUow()
        {
            Uow = new Uow(DbContext);
        }

        [Test]
        public void All_Returns_Items()
        {
            var user = new User
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.UtcNow
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();

            InitializeUow();
            var results = Uow.LegacyUserRepo.All();

            Assert.IsInstanceOf<IQueryable<User>>(results);
            Assert.AreEqual(1, results.Count());
        }

        [Test]
        public void Add_Insert_One_Item()
        {
            var user = new User
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.Now
            };

            InitializeUow();
            Uow.LegacyUserRepo.Add(user);
            Uow.Commit();

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual(1, DbContext.Users.Count());
        }

        [Test]
        public void Remove_Delete_One_Item()
        {
            var user = new User
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.UtcNow
            };
            DbContext.Users.Add(user);
            DbContext.SaveChanges();


            InitializeUow();
            user.UpdatedBy = 1;
            Uow.LegacyUserRepo.Remove(user);
            Uow.Commit();

            var removedItem = DbContext.Users.Single(x => x.Id == user.Id);
            Assert.AreEqual(1, DbContext.Users.Count());
            Assert.AreEqual(EntityStatusEnum.Removed, removedItem.Status);
        }

        [Test]
        public void Replace_Update_One_Item()
        {
            DbContext.Users.Add(new User
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.Now
            });
            DbContext.SaveChanges();

            //need to change as
            var user = new User
            {
                Name = "Admin1",
                Email = "Admin@gmail11.com",
                Password = "Admin1",
                Status = EntityStatusEnum.Inactive,
                UpdatedBy = 1
            };

            InitializeUow();
            var item = DbContext.Users.First();
            item.Name = user.Name;
            item.Email = user.Email;
            item.Password = user.Password;
            item.Status = user.Status;
            item.UpdatedBy = user.UpdatedBy;

            Uow.LegacyUserRepo.Replace(item);
            Uow.Commit();

            var updatedItem = DbContext.Users.First();
            Assert.AreEqual(user.Name, updatedItem.Name);
            Assert.AreEqual(user.Email, updatedItem.Email);
            Assert.AreEqual(user.Password, updatedItem.Password);
            Assert.AreEqual(user.Status, updatedItem.Status);
            Assert.AreEqual(user.UpdatedBy, updatedItem.UpdatedBy);
        }

        [TearDown]
        public void TearDown()
        {
            Uow.Dispose();
            Uow = null;
            DbContext = null;
        }
    }
}
