//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TrasferSystemTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestUserRepository
    {
        [Test]
        public void TestAdd()
        {
            var User = new User(_login: "Mukhamediev", _password_: "qwerty", _name_: "Joe", _surname: "Biden");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository rep = new UserRepository(context);
            
            rep.Add(User);

            User checkUser1 = rep.GetUserByLogin("Mukhamediev");

            Assert.IsNotNull(checkUser1, "Users was not added");
            Assert.AreEqual("Mukhamedievvv", checkUser1.Login, "Not equal Added User");
            Assert.AreEqual("qwerty", checkUser1.Password_, "Not equal Added User");
            Assert.AreEqual("Joe", checkUser1.Name_, "Not equal Added User");
            Assert.AreEqual("Biden", checkUser1.Surname, "Not equal Added User");

            rep.Delete(checkUser1);
        }

        [Test]
        public void TestGetAll()
        {
            var User = new User(_login: "Mukhamediev", _password_: "qwerty", _name_: "Joe", _surname: "Biden");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);
            
            List<User> Users = rep.GetAll();

            Assert.IsNotNull(Users, "Can't find Users");

            User checkUser2 = rep.GetUserByLogin("Mukhamediev");
            Assert.AreEqual("Mukhamediev", checkUser2.Login, "Not equal Added User");
            Assert.AreEqual("qwerty", checkUser2.Password_, "Not equal Added User");
            Assert.AreEqual("Joe", checkUser2.Name_, "Not equal Added User");
            Assert.AreEqual("Biden", checkUser2.Surname, "Not equal Added User");

            rep.Delete(checkUser2);
        }

        [Test]
        public void TestUpdate()
        {
            var User = new User(_login: "Mukhamediev", _password_: "qwerty", _name_: "Joe", _surname: "Biden");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);

            User newUser = new User(_login: "Mukhamediev", _password_: "qweasd", _name_: "Joe", _surname: "Biden");

            rep.Update(newUser);

            User checkUser2 = rep.GetUserByLogin(newUser.Login);

            Assert.IsNotNull(checkUser2, "cannot find User by id");
            Assert.AreEqual("Mukhamediev", newUser.Login, "Not equal added User");
            Assert.AreEqual("qweasd", checkUser2.Password_, "Not equal Added User");
            Assert.AreEqual("Joe", checkUser2.Name_, "Not equal Added User");
            Assert.AreEqual("Biden", checkUser2.Surname, "Not equal Added User");

            rep.Delete(checkUser2);
        }

        [Test]
        public void TestDelete()
        {
            var User = new User(_login: "Mukhamediev", _password_: "qwerty", _name_: "Joe", _surname: "Biden");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);

            rep.Delete(User);

            Assert.IsNull(rep.GetUserByLogin("Mukhamediev"), "User was not deleted");
        }

        [Test]
        public void TestGetUserByLogin()
        {
            var User = new User(_login: "Mukhamediev", _password_: "qwerty", _name_: "Joe", _surname: "Biden");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IUserRepository rep = new UserRepository(context);
            rep.Add(User);

            User checkUser1 = rep.GetUserByLogin("Mukhamediev");

            Assert.IsNotNull(checkUser1, "Users1 was not found");
            Assert.AreEqual("Mukhamediev", checkUser1.Login, "Not equal found User");
            Assert.AreEqual("qwerty", checkUser1.Password_, "Not equal found User");
            Assert.AreEqual("Joe", checkUser1.Name_, "Not equal found User");
            Assert.AreEqual("Biden", checkUser1.Surname, "Not equal found User");

            rep.Delete(checkUser1);
        }
    }
}
