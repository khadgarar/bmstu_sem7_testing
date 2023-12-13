//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Allure.Attributes;
using ComponentAccessToDB;

namespace IntegrationTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class IntTestEmployeeController
    {
        [Test]
        public void TestGetAllObjectives()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Objective> res = rep.GetAllObjectives();

            Assert.That(res.Count, Is.EqualTo(1), "GetAllObjectives Count");
            Assert.That(res[0].Objectiveid, Is.EqualTo(1), "GetAllObjectives Id");
            Assert.That(res[0].Parentobjective, Is.EqualTo(null), "GetAllObjectives Parentobjective");
            Assert.That(res[0].Title, Is.EqualTo("heh"), "GetAllObjectives Title");
        }

        [Test]
        public void TestGetWorkplace()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            WorkplaceView res = rep.GetWorkplace();

            Assert.That(res.EmployeeID, Is.EqualTo(5), "GetWorkplace Employee");
            Assert.That(res.Company.Companyid, Is.EqualTo(1), "GetWorkplace Company");
            Assert.That(res.Department, Is.EqualTo(null), "GetWorkplace Department");
        }

        [Test]
        public void TestGetAllDepartments()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Department> res = rep.GetAllDepartments();

            Assert.That(res.Count, Is.EqualTo(1), "GetAllDepartmentsCount");
            Assert.That(res[0].Departmentid, Is.EqualTo(1), "GetAllDepartments Id");
            Assert.That(res[0].Title, Is.EqualTo("Testing"), "GetAllDepartments Title");
        }

        [Test]
        public void TestGetAllEmployees()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<EmployeeView> res = rep.GetAllEmployees();

            Assert.That(res.Count, Is.EqualTo(5), "GetAllEmployees Count");
            Assert.That(res[0].Employeeid, Is.EqualTo(1), "GetAllEmployees Id1");
            Assert.That(res[1].Name_, Is.EqualTo("Donald"), "GetAllEmployees Name2");
        }

        [Test]
        public void TestGetObjectiveByID()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Objective> res = rep.GetObjectiveByID(1);

            Assert.That(res.Count, Is.EqualTo(1), "GetObjectiveByIDCount");
            Assert.That(res[0].Title, Is.EqualTo("heh"), "GetObjectiveByID Title");
        }

        [Test]
        public void TestGetObjectivesByTitle()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Objective> res = rep.GetObjectivesByTitle("heh");

            Assert.That(res.Count, Is.EqualTo(1), "GetObjectivesByTitle Count");
            Assert.That(res[0].Objectiveid, Is.EqualTo(1), "GetObjectivesByTitle Id");
        }

        [Test]
        public void TestGetObjectivesByTitleEmpty()
        {
            var user = new User("Daikatana", "empty", "Ilya", "Nikul");
            var employee = new Employee(5, "Daikatana", _permission_: 0);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new EmployeeController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Objective> res = rep.GetObjectivesByTitle("PPO");

            Assert.That(res.Count, Is.EqualTo(0), "GetObjectivesByTitleEmpty Count");
        }
    }
}

