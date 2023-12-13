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
    public class IntTestManagerController
    {
        [Test]
        public void TestAddObjective()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddObjective(null, "lol", new DateTime(), new DateTime(), new TimeSpan());

            var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Parentobjective, Is.EqualTo(null), "AddObjective Parentobjective");
            Assert.That(res.Title, Is.EqualTo("lol"), "AddObjective Title");

            ObjectiveRep.Delete(res);
        }

        [Test]
        public void TestUpdateObjective()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.UpdateObjective(1, "hehe", new DateTime(), new DateTime(), new TimeSpan(), null);

            var res = ObjectiveRep.GetObjectiveByID(1);
            Assert.That(res.Title, Is.EqualTo("hehe"), "UpdateObjective Title");

            rep.UpdateObjective(1, "heh", new DateTime(), new DateTime(), new TimeSpan(), null);
        }

        [Test]
        public void TestDeleteObjective()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ObjectiveRep.Add(new Objective(0, null, "lol"));
            var added = ObjectiveRep.GetAll().Last();

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteObjective(added.Objectiveid);

            var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Objectiveid, Is.Not.EqualTo(added.Objectiveid), "DeleteObjective");
        }

        [Test]
        public void TestGetResponsibleEmployees()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<EmployeeView> res = rep.GetResponsibleEmployees(1);

            Assert.AreEqual(res.Count, 0, "GetResponsibleEmployees Count");
        }

        [Test]
        public void TestAddResponsible()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddResponsible(2, 1);

            var res = ResponsibilityRep.GetAll().Last();
            Assert.That(res.Employee, Is.EqualTo(2), "AddResponsible Employee");
            Assert.That(res.Objective, Is.EqualTo(1), "AddResponsible Objective");

            ResponsibilityRep.Delete(res);
        }

        [Test]
        public void TestDeleteResponsibility()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteResponsibility(2, 1);

            var res = ResponsibilityRep.GetAll();

            Assert.That(res.Count, Is.EqualTo(0), "DeleteResponsibility");
        }

        [Test]
        public void TestGetResponsibilityByEmployee()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<Responsibility> res = rep.GetResponsibilityByEmployee(2);

            Assert.AreEqual(res.Count, 0, "GetResponsibilityByEmployee Count");
        }

        [Test]
        public void TestAddDepartment()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddDepartment("titlel", 1994, "descr");

            var res = DepartmentRep.GetAll().Last();
            Assert.That(res.Title, Is.EqualTo("titlel"), "AddDepartment Title");
            Assert.That(res.Activityfield, Is.EqualTo("descr"), "AddDepartment Activityfield");

            DepartmentRep.Delete(res);
        }

        [Test]
        public void TestUpdateDepartment()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.UpdateDepartment(1, "Testing", 1994, "descr");

            var res = DepartmentRep.GetDepartmentByID(1);
            Assert.That(res.Title, Is.EqualTo("Testing"), "UpdateDepartment Title");
            Assert.That(res.Activityfield, Is.EqualTo("descr"), "UpdateDepartment Activityfield");

            rep.UpdateDepartment(1, "Testing", 1994, "Information tech");
        }

        [Test]
        public void DeleteDepartment()
        {
            var user = new User("DarkBrandon", "qwerty", "Joe", "Biden");
            var employee = new Employee(1, "DarkBrandon", _permission_: 2);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            DepartmentRep.Add(new Department(0, "titlel", 1, 1994, "descr"));
            var added = DepartmentRep.GetAll().Last();

            var rep = new ManagerController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteDepartment(added.Departmentid);

            var res = DepartmentRep.GetAll().Last();
            Assert.That(res.Departmentid, Is.Not.EqualTo(added.Departmentid), "DeleteDepartment");
        }

    }
}

