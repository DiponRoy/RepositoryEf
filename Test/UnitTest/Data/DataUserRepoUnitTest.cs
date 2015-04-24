using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Data;
using Data.Repository;
using Db;
using Db.DbModel;
using Db.DbModel.Enum;
using FizzWare.NBuilder;
using Moq;
using NUnit.Framework;
using Test.Utility;

namespace Test.UnitTest.Data
{
    [TestFixture]
    public class DataUserRepoUnitTest
    {
        protected Mock<IPmsContext> DbContext { get; set; }
        protected IUow Uow { get; set; }

        [SetUp]
        public void SetUp()
        {
            DbContext = new Mock<IPmsContext>();
            DbContext.Setup(x => x.SaveChanges()).Returns(1);
        }

        protected void InitializeUow()
        {
            DbContext.Setup(x => x.Configuration).Returns(new PmsContextConfiguration());
            DbContext.Setup(x => x.EntryToAdd(It.IsAny<User>())).Returns(new PmsDbEntityEntry<User>(EntityState.Detached));
            DbContext.Setup(x => x.EntryToReplace(It.IsAny<User>())).Returns(new PmsDbEntityEntry<User>(EntityState.Detached));
            DbContext.Setup(x => x.Set<User>()).Returns(DbContext.Object.Users); //important to do here;
            Uow = new Uow(DbContext.Object);
        }

        [Test]
        public void All_Returns_Items()
        {
            List<User> users = Builder<User>.CreateListOfSize(3).Build().ToList();
            DbContext.Setup(x => x.Users).Returns(users.ToDbSet());
            InitializeUow();

            IQueryable<User> results = Uow.UserRepo.All();
            Assert.IsInstanceOf<IQueryable<User>>(results);
            Assert.AreEqual(3, results.Count());
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

            bool addedItem = false;
            DbContext.Setup(x => x.Users.Add(It.IsAny<User>())).Callback(() => { addedItem = true; });
            InitializeUow();
            Uow.UserRepo.Add(user);
            Uow.Commit();

            Assert.IsTrue(addedItem);
        }


        [Test]
        public void Add_Inserted_Item_Fields()
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

            User addedItem = null;
            DbContext.Setup(x => x.Users.Add(It.IsAny<User>())).Callback((User aUser) => { addedItem = aUser; });
            InitializeUow();
            Uow.UserRepo.Add(user);
            Uow.Commit();

            Assert.IsNotNull(addedItem);
            Assert.AreEqual(user.Name, addedItem.Name);
            Assert.AreEqual(user.Email, addedItem.Email);
            Assert.AreEqual(user.Password, addedItem.Password);
            Assert.AreEqual(user.Status, addedItem.Status);
            Assert.AreEqual(user.AddedBy, addedItem.AddedBy);
            Assert.AreEqual(user.AddedDateTime, addedItem.AddedDateTime);
        }

        [Test]
        public void Remove_Delete_One_Item()
        {
            var user = new User
            {
                Id = 1,
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.UtcNow
            };

            DbContext.Setup(x => x.Users).Returns(new List<User> {user}.ToDbSet());
            InitializeUow();

            user.UpdatedBy = 1;
            Uow.UserRepo.Remove(user);
            Uow.Commit();

            User removedItem = DbContext.Object.Users.Single(x => x.Id == user.Id);
            Assert.AreEqual(1, DbContext.Object.Users.Count());
            Assert.AreEqual(EntityStatusEnum.Removed, removedItem.Status);
        }

        [Test]
        public void Replace_Update_One_Item()
        {
            var oldUser = new User
            {
                Id = 1,
                Name = "Admin",
                Email = "Admin@gmail.com",
                Password = "Admin",
                Status = EntityStatusEnum.Active,
                AddedBy = 1,
                AddedDateTime = DateTime.UtcNow
            };
            DbContext.Setup(x => x.Users).Returns(new List<User> { oldUser }.ToDbSet());


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
            User item = DbContext.Object.Users.First();
            item.Name = user.Name;
            item.Email = user.Email;
            item.Password = user.Password;
            item.Status = user.Status;
            item.UpdatedBy = user.UpdatedBy;

            Uow.UserRepo.Replace(item);
            Uow.Commit();

            User updatedItem = DbContext.Object.Users.First();
            Assert.AreEqual(user.Name, updatedItem.Name);
            Assert.AreEqual(user.Email, updatedItem.Email);
            Assert.AreEqual(user.Password, updatedItem.Password);
            Assert.AreEqual(user.Status, updatedItem.Status);
            Assert.AreEqual(user.UpdatedBy, updatedItem.UpdatedBy);
        }

        [Test]
        public void Self_Returns_IQueryable()
        {
            List<User> users = Builder<User>.CreateListOfSize(3).Build().ToList();
            DbContext.Setup(x => x.Users).Returns(users.ToDbSet());
            InitializeUow();

            IDataRepository<User> results = Uow.UserRepo;
            Assert.IsInstanceOf<IQueryable<User>>(results);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void Self_Apply_Single()
        {
            List<User> users = Builder<User>.CreateListOfSize(3).Build().ToList();
            DbContext.Setup(x => x.Users).Returns(users.ToDbSet());
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
