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
    public class IntTestFounderController
    {
        [Test]
        public void TestAddEmployee()
        {
            var user = new User("Khadgar", "none", "Roma", "Mucha");
            var employee = new Employee(4, "Khadgar", _permission_: 4);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddEmployee("Daikatana", 1, 1);

            var res = EmployeeRep.GetAll().Last();
            Assert.That(res.User_, Is.EqualTo("Daikatana"), "AddEmployee User_");
            Assert.That(res.Permission_, Is.EqualTo(1), "AddEmployee Permission_");
            Assert.That(res.Department, Is.EqualTo(1), "AddEmployee Department");

            EmployeeRep.Delete(res);
        }

        [Test]
        public void TestDeleteEmployee()
        {
            var user = new User("Khadgar", "none", "Roma", "Mucha");
            var employee = new Employee(4, "Khadgar", _permission_: 4);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            EmployeeRep.Add(new Employee(0, "Daikatana", 1, 1));
            var added = EmployeeRep.GetAll().Last();

            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteEmployee(added.Employeeid);

            var res = EmployeeRep.GetAll().Last();
            Assert.That(res.Employeeid, Is.Not.EqualTo(added.Employeeid), "DeleteEmployee");
        }

        [Test]
        public void TestUpdateCompany()
        {
            var user = new User("Khadgar", "none", "Roma", "Mucha");
            var employee = new Employee(4, "Khadgar", _permission_: 4);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.UpdateCompany("new name", 1994);

            var res = CompanyRep.GetCompanyByID(1);
            Assert.That(res.Title, Is.EqualTo("new name"), "UpdateCompany Title");

            rep.UpdateCompany("tilt", 1994);
        }

        [Test]
        public void TestDeleteCompany()
        {
            var user = new User("Khadgar", "none", "Roma", "Mucha");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            CompanyRep.Add(new Company(0, "wow corp", 1999));
            EmployeeRep.Add(new Employee(0, "Khadgar", 2, null, 4));

            var employee = new Employee(6, "Khadgar", 2, null, 4);

            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteCompany();

            var res = CompanyRep.GetAll();
            Assert.That(res.Count, Is.EqualTo(1), "DeleteCompany");
        }
    }
}

