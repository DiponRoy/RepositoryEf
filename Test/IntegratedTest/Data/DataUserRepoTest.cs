using System;
using System.Linq;
using Data;
using Data.Repository;
using Db;
using Db.DbModel;
using Db.DbModel.Enum;
using NUnit.Framework;

namespace Test.IntegratedTest.Data
{
    [TestFixture]
    public class DataUserRepoTest
    {
        protected IPmsContext DbContext { get; set; }
        protected IUow Uow { get; set; }

        [SetUp]
        public void SetUp()
        {
            DbContext = new BuildContext();
        }

        protected void InitializeUow()
        {
            Uow = new Uow(DbContext);
        }

        [Test, Explicit]
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
            IQueryable<User> results = Uow.UserRepo.All();

            Assert.IsInstanceOf<IQueryable<User>>(results);
            Assert.AreEqual(1, results.Count());
        }

        [Test, Explicit]
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
            Uow.UserRepo.Add(user);
            Uow.Commit();

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual(1, DbContext.Users.Count());
        }

        [Test, Explicit]
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
            Uow.UserRepo.Remove(user);
            Uow.Commit();

            User removedItem = DbContext.Users.Single(x => x.Id == user.Id);
            Assert.AreEqual(1, DbContext.Users.Count());
            Assert.AreEqual(EntityStatusEnum.Removed, removedItem.Status);
        }

        [Test, Explicit]
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
            User item = DbContext.Users.First();
            item.Name = user.Name;
            item.Email = user.Email;
            item.Password = user.Password;
            item.Status = user.Status;
            item.UpdatedBy = user.UpdatedBy;

            Uow.UserRepo.Replace(item);
            Uow.Commit();

            User updatedItem = DbContext.Users.First();
            Assert.AreEqual(user.Name, updatedItem.Name);
            Assert.AreEqual(user.Email, updatedItem.Email);
            Assert.AreEqual(user.Password, updatedItem.Password);
            Assert.AreEqual(user.Status, updatedItem.Status);
            Assert.AreEqual(user.UpdatedBy, updatedItem.UpdatedBy);
        }

        [Test, Explicit]
        public void Self_Returns_IQueryable()
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
            IDataRepository<User> results = Uow.UserRepo;

            Assert.IsInstanceOf<IQueryable<User>>(results);
            Assert.AreEqual(1, results.Count());
        }

        [Test, Explicit]
        public void Self_Apply_Single()
        {
            DbContext.Users.Add(new User
            {
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.UtcNow
            });
            DbContext.SaveChanges();

            InitializeUow();
            User result = Uow.UserRepo.First();

            Assert.IsNotNull(result);
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
