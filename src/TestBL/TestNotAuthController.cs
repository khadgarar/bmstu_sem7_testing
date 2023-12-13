//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using Moq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestNotAuthController
    {
        [Test]
        public void TestGetUserByLogin()
        {
            var UserRep = new Mock<IUserRepository>();

            UserRep.Setup(x => x.GetUserByLogin("login"))
                .Returns(new User("login", _name_: "creative"));

            var rep = new NotAuthController(UserRep.Object);

            User res = rep.GetUserByLogin("login");

            Assert.That(res.Login, Is.EqualTo("login"), "GetUserByLoginLogin");
            Assert.That(res.Name_, Is.EqualTo("creative"), "GetUserByLoginName");
        }

        [Test]
        public void TestGetUserByLoginNull()
        {
            var UserRep = new Mock<IUserRepository>();

            UserRep.Setup(x => x.GetUserByLogin("login"))
                .Returns((User)null);

            var rep = new NotAuthController(UserRep.Object);

            User res = rep.GetUserByLogin("login");

            Assert.That(res, Is.EqualTo(null), "GetUserByLoginNull");
        }

        [Test]
        public void TestAddUser()
        {
            var UserRep = new Mock<IUserRepository>();

            var rep = new NotAuthController(UserRep.Object);

            rep.AddUser("login", "", "creative", "");

            UserRep.Verify(x => x.Add(It.Is<User>(x =>
                x.Login == "login" && x.Password_ == "" && x.Name_ == "creative" && x.Surname == "")),
                Times.Once);
        }
    }
}

