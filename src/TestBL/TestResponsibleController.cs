//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using System.Collections.Generic;
using Moq;
using System;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestResponsibleController
    {
        [Test]
        public void TestAddSubObjective()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(), new Employee(4) });

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.AddSubObjective(1, "heh", new DateTime(), new DateTime(), new TimeSpan());

            ObjectiveRep.Verify(x => x.Add(It.Is<Objective>(x =>
                x.Objectiveid == 0 && x.Parentobjective == 1 && x.Title == "heh" && x.Company == 1 && x.Department == null)),
                Times.Once);
        }

        [Test]
        public void TestUpdateObjective()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(), new Employee(4) });

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.UpdateObjective(1, "heh", new DateTime(), new DateTime(), new TimeSpan());

            ObjectiveRep.Verify(x => x.Update(It.Is<Objective>(x =>
                x.Objectiveid == 1 && x.Parentobjective == null && x.Title == "heh" && x.Company == 1 && x.Department == null)),
                Times.Once);
        }

        [Test]
        public void TestDeleteSubObjective()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(), new Employee(4) });

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective(_parentobjective: 2));

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.DeleteSubObjective(1);

            ObjectiveRep.Verify(x => x.Delete(It.Is<Objective>(x =>
                x.Objectiveid == 1 && x.Parentobjective == 2)),
                Times.Once);
        }

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

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<EmployeeView> res = rep.GetResponsibleEmployees(1);

            Assert.AreEqual(res.Count, 2, "GetResponsibleEmployeesCount");
            Assert.AreEqual(res[0].Employeeid, 1, "GetResponsibleEmployeesId1");
            Assert.AreEqual(res[0].Login, "hello", "GetResponsibleEmployeesLogin1");
            Assert.AreEqual(res[1].Employeeid, 4, "GetResponsibleEmployeesId2");
            Assert.AreEqual(res[1].Name_, "name", "GetResponsibleEmployeesName2");
        }

        [Test]
        public void TestAddResponsibility()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(), new Employee(2) });

            EmployeeRep.Setup(x => x.GetEmployeeByID(4))
                .Returns(new Employee(4, "login"));

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            ResponsibilityRep.Setup(x => x.GetResponsibilityByObjectiveAndEmployee(1, 4))
                .Returns((Responsibility)null);

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.AddResponsibility(4, 1, new TimeSpan());

            ResponsibilityRep.Verify(x => x.Add(It.Is<Responsibility>(x =>
                x.Responsibilityid == 0 && x.Employee == 4 && x.Objective == 1)),
                Times.Once);
        }

        [Test]
        public void TestDeleteResponsibility()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetResponsibleEmployees(1))
                .Returns(new List<Employee>() { new Employee(), new Employee(2) });

            EmployeeRep.Setup(x => x.GetEmployeeByID(4))
                .Returns(new Employee(4, "login"));

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            ResponsibilityRep.Setup(x => x.GetResponsibilityByObjectiveAndEmployee(1, 4))
                .Returns(new Responsibility(1, 4, 1));

            var rep = new ResponsibleController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.DeleteResponsibility(4, 1);

            ResponsibilityRep.Verify(x => x.Delete(It.Is<Responsibility>(x =>
                x.Responsibilityid == 1 && x.Employee == 4 && x.Objective == 1)),
                Times.Once);
        }
    }
}

