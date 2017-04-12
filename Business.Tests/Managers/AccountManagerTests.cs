using BLL.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TeamProject.DAL;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using System;

namespace BLL.Managers.Tests
{
    [TestClass()]
    public class AccountManagerTests
    {
        Mock<ICinemaWork> fakeWork = new Mock<ICinemaWork>();

        [TestMethod()]
        public void GetUserEmailAndNameBoth()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> { new User { ID = 1, Name = "A", Email = "A@mil.ru" } }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser("A");
            User resUser2 = manager.GetUser("A@mil.ru");
            Assert.AreEqual(resUser, resUser2);
        }
        [TestMethod()]
        public void GetUserEmailPasswordCorrect()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> {
                new User { ID = 1, Name = "A", Email = "A@mil.ru", Password = "12345" },
                new User { ID = 2, Name = "B", Email = "B@mil.ru", Password = "22222" },
                new User { ID = 3, Name = "A", Email = "A@mial.ru", Password = "55555" } ,
                new User { ID = 4, Name = "A", Email = "A@mibl.ru", Password = "55455" } ,
            }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser("A@mil.ru", "12345");
            User resUser4 = manager.GetUser("A", "55555");
            User resUser41 = manager.GetUser("A", "12345");
            User resUser5 = manager.GetUser("A@mial.ru", "55555");
            User resUser6 = manager.GetUser("A@mibl.ru", "55455");
            Assert.AreEqual(1, resUser.ID);
            Assert.AreEqual(3, resUser4.ID);
            Assert.AreEqual(1, resUser41.ID);
            Assert.AreEqual(3, resUser5.ID);
            Assert.AreEqual(4, resUser6.ID);

        }
        [TestMethod]
        public void GetUserNotFoundUser()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> { new User { ID = 1, Name = "A", Email = "A@mil.ru" } }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser("B", "12");
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetUserMoreThanOneNameFind()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> {
                new User { ID = 1, Name = "pauluxxx", Email = "pauluxxx@mail.ru", Password = "55555" },
                new User { ID = 4, Name = "pauluxxx", Email = "pauluxxx@gmail.com", Password = "55555" } ,
            }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser("pauluxxx", "55555");
        }
        [TestMethod]
        public void GetUserEmptyItems()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User>
            {
            }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            var s = manager.GetUser("A", "s");
        }
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetUserMoreThanOneEmailFind()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> {
                new User { ID = 1, Name = "As", Email = "A@mibl.ru", Password = "55555" },
                new User { ID = 4, Name = "A", Email = "A@mibl.ru", Password = "55555" } ,
            }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser("A@mibl.ru", "55555");
        }

        [TestMethod()]
        public void GetUserId()
        {
            fakeWork.Setup(m => m.Users.Items).Returns(new List<User> {
                new User { ID = 1, Name = "As", Email = "A@mibl.ru", Password = "55555" },
                new User { ID = 4, Name = "A", Email = "A@mibl.ru", Password = "55555" } ,
            }.AsQueryable());
            AccountManager manager = new AccountManager(fakeWork.Object);
            User resUser = manager.GetUser(1);
            Assert.AreEqual("As", resUser.Name);
        }
        [TestMethod()]
        public void CheckUserPasswordValid()
        {
            User user = new User { Password = "1234" };
            AccountManager manager = new AccountManager(fakeWork.Object);
            bool yes = manager.CheckUserPassword(user, "1234");
            Assert.IsTrue(yes);
        }
        [TestMethod()]
        public void CheckUserPasswordInvalid()
        {
            User user = new User { Password = "1234" };
            AccountManager manager = new AccountManager(fakeWork.Object);
            bool yes = manager.CheckUserPassword(user, "12");
            Assert.IsFalse(yes);
        }

    }
}