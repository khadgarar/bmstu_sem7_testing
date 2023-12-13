//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using Moq;
using AutoFixture;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestUserController
    {
        [Test]
        public void TestGetUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "login")
                .With(x => x.Name_, "creative").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            UserView res = rep.GetUser();

            Assert.That(res.Login, Is.EqualTo("login"), "GetUserLogin");
            Assert.That(res.Name_, Is.EqualTo("creative"), "GetUserName");
        }

        [Test]
        public void TestAddCompany()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "login").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            CompanyRep.Setup(x => x.GetAll())
                .Returns(new List<Company>() { new Company(), new Company(4) });

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            rep.AddCompany("title", 1994);

            CompanyRep.Verify(x => x.Add(It.Is<Company>(x =>
                x.Companyid == 0 && x.Title == "title" && x.Foundationyear == 1994)),
                Times.Once);

            EmployeeRep.Verify(x => x.Add(It.Is<Employee>(x =>
                x.Employeeid == 0 && x.User_ == "login" && x.Company == 4 && x.Department == null && x.Permission_ == (int)Permissions.Founder)),
                Times.Once);
        }

        [Test]
        public void TestGetWorkplaces()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "hello").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetAll())
                .Returns(new List<Employee>() { new Employee(2, "hello"), new Employee(4, "world", 4) });

            CompanyRep.Setup(x => x.GetCompanyByID(1))
                .Returns(new Company(4));

            DepartmentRep.Setup(x => x.GetDepartmentByID(null))
                .Returns(new Department(5));

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            List<WorkplaceView> res = rep.GetWorkplaces();

            Assert.That(res.Count, Is.EqualTo(1), "GetWorkplacesCount");
            Assert.That(res[0].EmployeeID, Is.EqualTo(2), "GetWorkplacesEmployee");
            Assert.That(res[0].Company.Companyid, Is.EqualTo(4), "GetWorkplacesCompany");
            Assert.That(res[0].Department.Departmentid, Is.EqualTo(5), "GetWorkplacesDepartment");
        }

        [Test]
        public void TestGetWorkplacesEmpty()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "hello").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetAll())
                .Returns(new List<Employee>());

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            List<WorkplaceView> res = rep.GetWorkplaces();

            Assert.That(res.Count, Is.EqualTo(0), "GetWorkplacesEmptyCount");
        }

        [Test]
        public void TestGetEmployeeByWorkplace()
        {
            var user = new User();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetEmployeeByID(4))
                .Returns(new Employee(4, "login"));

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            Employee res = rep.GetEmployeeByWorkplace(4);

            Assert.That(res.Employeeid, Is.EqualTo(4), "GetEmployeeByWorkplaceEmployee");
            Assert.That(res.User_, Is.EqualTo("login"), "GetEmployeeByWorkplaceUser");
        }

        [Test]
        public void TestUpdateUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "hello").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            rep.UpdateUser("qwe", "new", "sur");

            UserRep.Verify(x => x.Update(It.Is<User>(x =>
                x.Login == "hello" && x.Password_ == "qwe" && x.Name_ == "new" && x.Surname == "sur")),
                Times.Once);
        }

        [Test]
        public void TestDeleteUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "hello").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            UserRep.Setup(x => x.GetUserByLogin("hello"))
                .Returns(new User("hello", _name_: "creative"));

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            rep.DeleteUser();

            UserRep.Verify(x => x.Delete(It.Is<User>(x =>
                x.Login == "hello" && x.Password_ == "" && x.Name_ == "creative" && x.Surname == null)),
                Times.Once);
        }

        [Test]
        public void TestDeleteFakeUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>().With(x => x.Login, "hello").Create();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            UserRep.Setup(x => x.GetUserByLogin("hello"))
                .Returns((User)null);

            var rep = new UserController(
                user, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object);

            rep.DeleteUser();

            UserRep.Verify(x => x.Delete(It.IsAny<User>()),
                Times.Never);
        }
    }
}

