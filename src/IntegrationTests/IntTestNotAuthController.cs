//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class IntTestNotAuthController
    {
        [Test]
        public void TestGetUserByLogin()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            User res = rep.GetUserByLogin("DarkBrandon");

            Assert.That(res.Login, Is.EqualTo("DarkBrandon"), "GetUserByLogin Login");
            Assert.That(res.Name_, Is.EqualTo("Joe"), "GetUserByLogin Name");
        }

        [Test]
        public void TestGetUserByLoginNull()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            User res = rep.GetUserByLogin("login");

            Assert.That(res, Is.EqualTo(null), "GetUserByLoginNull");
        }

        [Test]
        public void TestAddUser()
        {
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IUserRepository UserRep = new UserRepository(context);

            var rep = new NotAuthController(UserRep);

            rep.AddUser("mucha", "", "Rowoma", "");

            User res = rep.GetUserByLogin("mucha");

            Assert.That(res.Login, Is.EqualTo("mucha"), "AddUserLogin");
            Assert.That(res.Name_, Is.EqualTo("Rowoma"), "AddUserName");

            UserRep.Delete(res);
        }
    }
}

