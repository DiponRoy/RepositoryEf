using System;
using System.Collections.Generic;
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

        //[Test]
        //public void Add_Insert_One_Item()
        //{
        //    var user = new User
        //    {
        //        Name = "Admin",
        //        Email = "Admin@gmail.com",
        //        Password = "Admin",
        //        Status = EntityStatusEnum.Active,
        //        AddedBy = 1,
        //        AddedDateTime = DateTime.Now
        //    };

        //    List<User> users = new List<User>();
        //    DbContext.Setup(x => x.Users).Returns(users.ToDbSet());
        //    InitializeUow();
        //    Uow.UserRepo.Add(user);
        //    Uow.Commit();

        //    Assert.AreEqual(1, users.Count());
        //}

        //[Test]
        //public void Remove_Delete_One_Item()
        //{
        //    var user = new User
        //    {
        //        Name = "Admin",
        //        Email = "Admin@gmail.com",
        //        Password = "Admin",
        //        Status = EntityStatusEnum.Active,
        //        AddedBy = 1,
        //        AddedDateTime = DateTime.UtcNow
        //    };
        //    DbContext.Users.Add(user);
        //    DbContext.SaveChanges();


        //    InitializeUow();
        //    user.UpdatedBy = 1;
        //    Uow.UserRepo.Remove(user);
        //    Uow.Commit();

        //    User removedItem = DbContext.Users.Single(x => x.Id == user.Id);
        //    Assert.AreEqual(1, DbContext.Users.Count());
        //    Assert.AreEqual(EntityStatusEnum.Removed, removedItem.Status);
        //}

        //[Test]
        //public void Replace_Update_One_Item()
        //{
        //    DbContext.Users.Add(new User
        //    {
        //        Name = "Admin",
        //        Email = "Admin@gmail.com",
        //        Password = "Admin",
        //        Status = EntityStatusEnum.Active,
        //        AddedBy = 1,
        //        AddedDateTime = DateTime.Now
        //    });
        //    DbContext.SaveChanges();

        //    //need to change as
        //    var user = new User
        //    {
        //        Name = "Admin1",
        //        Email = "Admin@gmail11.com",
        //        Password = "Admin1",
        //        Status = EntityStatusEnum.Inactive,
        //        UpdatedBy = 1
        //    };

        //    InitializeUow();
        //    User item = DbContext.Users.First();
        //    item.Name = user.Name;
        //    item.Email = user.Email;
        //    item.Password = user.Password;
        //    item.Status = user.Status;
        //    item.UpdatedBy = user.UpdatedBy;

        //    Uow.UserRepo.Replace(item);
        //    Uow.Commit();

        //    User updatedItem = DbContext.Users.First();
        //    Assert.AreEqual(user.Name, updatedItem.Name);
        //    Assert.AreEqual(user.Email, updatedItem.Email);
        //    Assert.AreEqual(user.Password, updatedItem.Password);
        //    Assert.AreEqual(user.Status, updatedItem.Status);
        //    Assert.AreEqual(user.UpdatedBy, updatedItem.UpdatedBy);
        //}

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
