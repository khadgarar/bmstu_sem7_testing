//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using Moq;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Allure.Attributes;
using AutoFixture;
using System.Collections.Generic;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestEmployeeController
    {
        [Test]
        public void TestGetAllObjectives()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetAll())
                .Returns(new List<Objective>() { new Objective(), new Objective(4) });

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetAllObjectives();

            Assert.That(res.Count, Is.EqualTo(2), "GetAllObjectivesCount");
            Assert.That(res[0].Objectiveid, Is.EqualTo(1), "GetAllObjectivesId1");
            Assert.That(res[1].Objectiveid, Is.EqualTo(4), "GetAllObjectivesId2");
        }

        [Test]
        public void TestGetAllObjectivesEmpty()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetAll())
                .Returns(new List<Objective>());

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetAllObjectives();

            Assert.That(res.Count, Is.EqualTo(0), "GetAllObjectivesEmpty");
        }

        [Test]
        public void TestGetWorkplace()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            CompanyRep.Setup(x => x.GetCompanyByID(1))
                .Returns(new Company(4));

            DepartmentRep.Setup(x => x.GetDepartmentByID(null))
                .Returns(new Department(5));

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            WorkplaceView res = rep.GetWorkplace();

            Assert.That(res.EmployeeID, Is.EqualTo(1), "GetWorkplaceEmployee");
            Assert.That(res.Company.Companyid, Is.EqualTo(4), "GetWorkplaceCompany");
            Assert.That(res.Department.Departmentid, Is.EqualTo(5), "GetWorkplaceDepartment");
        }

        [Test]
        public void TestGetAllDepartments()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            DepartmentRep.Setup(x => x.GetDepartmentsByCompany(1))
                .Returns(new List<Department>() { new Department(), new Department(4) });

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Department> res = rep.GetAllDepartments();

            Assert.That(res.Count, Is.EqualTo(2), "GetAllDepartmentsCount");
            Assert.That(res[0].Departmentid, Is.EqualTo(1), "GetAllDepartmentsId1");
            Assert.That(res[1].Departmentid, Is.EqualTo(4), "GetAllDepartmentsId2");
        }

        [Test]
        public void TestGetAllEmployees()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            EmployeeRep.Setup(x => x.GetAll())
                .Returns(new List<Employee>() { new Employee(2, "hello"), new Employee(4, "world", 4) });

            UserRep.Setup(x => x.GetUserByLogin("hello"))
                .Returns(new User("hello", "", "Name"));

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<EmployeeView> res = rep.GetAllEmployees();

            Assert.That(res.Count, Is.EqualTo(1), "GetAllEmployeesCount");
            Assert.That(res[0].Employeeid, Is.EqualTo(2), "GetAllEmployeesId");
            Assert.That(res[0].Name_, Is.EqualTo("Name"), "GetAllEmployeesName");
        }

        [Test]
        public void TestGetObjectiveByID()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            ObjectiveRep.Setup(x => x.GetSubObjectives(1))
                .Returns(new List<Objective>() { new Objective(2), new Objective(4, _company: 4) });

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetObjectiveByID(1);

            Assert.That(res.Count, Is.EqualTo(2), "GetObjectiveByIDCount");
            Assert.That(res[0].Objectiveid, Is.EqualTo(1), "GetObjectiveByIDId1");
            Assert.That(res[1].Objectiveid, Is.EqualTo(2), "GetObjectiveByIDId2");
        }

        [Test]
        public void TestGetObjectiveByIDNoSubObjectives()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetObjectiveByID(1))
                .Returns(new Objective());

            ObjectiveRep.Setup(x => x.GetSubObjectives(1))
                .Returns(new List<Objective>());

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetObjectiveByID(1);

            Assert.That(res.Count, Is.EqualTo(1), "GetObjectiveByIDNoSubObjectivesCount");
            Assert.That(res[0].Objectiveid, Is.EqualTo(1), "GetObjectiveByIDNoSubObjectives");
        }

        [Test]
        public void TestGetObjectivesByTitle()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetObjectivesByTitle("PPO"))
                .Returns(new List<Objective>() { new Objective(2), new Objective(4, _company: 4) });

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetObjectivesByTitle("PPO");

            Assert.That(res.Count, Is.EqualTo(1), "GetObjectivesByTitleCount");
            Assert.That(res[0].Objectiveid, Is.EqualTo(2), "GetObjectivesByTitleId");
        }

        [Test]
        public void TestGetObjectivesByTitleEmpty()
        {
            var user = new User();
            var employee = new Employee();
            var EmployeeRep = new Mock<IEmployeeRepository>();
            var ResponsibilityRep = new Mock<IResponsibilityRepository>();
            var ObjectiveRep = new Mock<IObjectiveRepository>();
            var CompanyRep = new Mock<ICompanyRepository>();
            var DepartmentRep = new Mock<IDepartmentRepository>();
            var UserRep = new Mock<IUserRepository>();

            ObjectiveRep.Setup(x => x.GetObjectivesByTitle("PPO"))
                .Returns(new List<Objective>());

            var rep = new EmployeeController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            List<Objective> res = rep.GetObjectivesByTitle("PPO");

            Assert.That(res.Count, Is.EqualTo(0), "GetObjectivesByTitleEmptyCount");
        }
    }
}

