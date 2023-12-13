//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using System.Collections.Generic;
using Moq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestHRController
    {
        [Test]
        public void TestGetResponsibleEmployees()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            UserRep.Setup(x => x.GetUserByLogin("hello"))
                .Returns(new User("hello", _name_: "creative"));

            UserRep.Setup(x => x.GetUserByLogin("world"))
                .Returns(new User("world", _name_: "name"));

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(_user_: "hello"), new Employee(4, _user_: "world") });

            var rep = new HRController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<EmployeeView> res = rep.GetResponsibleEmployees(1);

            Assert.AreEqual(res.Count, 2, "GetResponsibleEmployeesCount");
            Assert.AreEqual(res[0].Login, "hello", "GetResponsibleEmployeesLogin1");
            Assert.AreEqual(res[1].Name_, "name", "GetResponsibleEmployeesName2");
        }

        [Test]
        public void TestAddEmployee()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            var rep = new HRController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.AddEmployee("heh", 0, null);

            EmployeeRep.Verify(x => x.Add(It.Is<Employee>(x =>
                x.Employeeid == 0 && x.User_ == "heh" && x.Company == 1 && x.Department == null && x.Permission_ == 0)),
                Times.Once);
        }

        [Test]
        public void TestUpdateEmployee()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetEmployeeByID(2))
                .Returns(new Employee(2));

            var rep = new HRController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.UpdateEmployee(2, "heh", 0, null);

            EmployeeRep.Verify(x => x.Update(It.Is<Employee>(x =>
                x.Employeeid == 2 && x.User_ == "heh" && x.Company == 1 && x.Department == null && x.Permission_ == 0)),
                Times.Once);
        }

        [Test]
        public void TestDeleteEmployee()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetEmployeeByID(2))
                .Returns(new Employee(2));

            var rep = new HRController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.DeleteEmployee(2);

            EmployeeRep.Verify(x => x.Delete(It.Is<Employee>(x =>
                x.Employeeid == 2)),
                Times.Once);
        }
    }
}

