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
    public class IntTestResponsibleController
    {
        [Test]
        public void TestAddSubObjective()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));
            var addedResp = ResponsibilityRep.GetAll().Last();

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddSubObjective(1, "lol", new DateTime(), new DateTime(), new TimeSpan());

            var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Parentobjective, Is.EqualTo(1), "AddSubObjective Parentobjective");
            Assert.That(res.Title, Is.EqualTo("lol"), "AddSubObjective Title");

            ObjectiveRep.Delete(res);
            ResponsibilityRep.Delete(addedResp);
        }

        [Test]
        public void TestUpdateObjective()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));
            var addedResp = ResponsibilityRep.GetAll().Last();

            ObjectiveRep.Add(new Objective(0, 1, "lol"));
            var added = ObjectiveRep.GetAll().Last();

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.UpdateObjective(added.Objectiveid, "omegalol", new DateTime(), new DateTime(), new TimeSpan());

            var res = ObjectiveRep.GetObjectiveByID(added.Objectiveid);
            Assert.That(res.Title, Is.EqualTo("omegalol"), "UpdateObjective Title");

            ObjectiveRep.Delete(res);
            ResponsibilityRep.Delete(addedResp);
        }

        [Test]
        public void TestDeleteSubObjective()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));
            var addedResp = ResponsibilityRep.GetAll().Last();

            ObjectiveRep.Add(new Objective(0, 1, "lol"));
            var added = ObjectiveRep.GetAll().Last();

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteSubObjective(added.Objectiveid);

            var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Objectiveid, Is.Not.EqualTo(added.Objectiveid), "DeleteSubObjective");

            ResponsibilityRep.Delete(addedResp);
        }

        [Test]
        public void TestGetResponsibleEmployees()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));
            var addedResp = ResponsibilityRep.GetAll().Last();

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            List<EmployeeView> res = rep.GetResponsibleEmployees(1);

            Assert.AreEqual(res.Count, 1, "GetResponsibleEmployees Count");

            ResponsibilityRep.Delete(addedResp);
        }

        [Test]
        public void TestAddResponsibility()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddResponsibility(2, 1, new TimeSpan());

            var res = ResponsibilityRep.GetAll();
            Assert.That(res.Count, Is.EqualTo(0), "AddResponsibility");
        }

        [Test]
        public void TestDeleteResponsibility()
        {
            var user = new User("MAGAa", "strong", "Donald", "Trump");
            var employee = new Employee(2, "MAGAa", _permission_: 1);

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IEmployeeRepository EmployeeRep = new EmployeeRepository(context);
            IResponsibilityRepository ResponsibilityRep = new ResponsibilityRepository(context);
            IObjectiveRepository ObjectiveRep = new ObjectiveRepository(context);
            ICompanyRepository CompanyRep = new CompanyRepository(context);
            IDepartmentRepository DepartmentRep = new DepartmentRepository(context);
            IUserRepository UserRep = new UserRepository(context);

            ResponsibilityRep.Add(new Responsibility(0, 2, 1));

            var rep = new ResponsibleController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteResponsibility(2, 1);

            var res = ResponsibilityRep.GetAll();

            Assert.That(res.Count, Is.EqualTo(0), "DeleteResponsibility");
        }
    }
}

