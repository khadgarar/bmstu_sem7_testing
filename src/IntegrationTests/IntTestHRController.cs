//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentBuisinessLogic;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using ComponentAccessToDB;

namespace IntegrationTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class IntTestHRController
    {
        [Test]
        public void TestGetResponsibleEmployees()
        {
            var user = new User("SqueakBug", "nice", "Artem", "Lisnevsky");
            var employee = new Employee(3, "SqueakBug", _permission_: 3);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new HRController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<EmployeeView> res = rep.GetResponsibleEmployees(1);

            Assert.AreEqual(res.Count, 0, "GetResponsibleEmployees Count");
        }

        [Test]
        public void TestAddEmployee()
        {
            var user = new User("SqueakBug", "nice", "Artem", "Lisnevsky");
            var employee = new Employee(3, "SqueakBug", _permission_: 3);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new HRController(
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
            var user = new User("SqueakBug", "nice", "Artem", "Lisnevsky");
            var employee = new Employee(3, "SqueakBug", _permission_: 3);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            EmployeeRep.Add(new Employee(0, "Daikatana", 1, 1));
            var added = EmployeeRep.GetAll().Last();

            var rep = new HRController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteEmployee(added.Employeeid);

            var res = EmployeeRep.GetAll().Last();
            Assert.That(res.Employeeid, Is.Not.EqualTo(added.Employeeid), "DeleteEmployee");
        }
    }
}

