//using Microsoft.VisualStudio.TestTools.UnitTesting;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace E2ETest
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class E2ETest
    {
        private User user;
        private Employee employee;

        private IEmployeeRepository EmployeeRep;
        private IResponsibilityRepository ResponsibilityRep;
        private IObjectiveRepository ObjectiveRep;
        private ICompanyRepository CompanyRep;
        private IDepartmentRepository DepartmentRep;
        private IUserRepository UserRep;

        [Test]
        public void MakeCompanyWithTaskAndEmployee()
        {
            user = new User("DarkBrandon", "qwerty", "Joe", "Biden");

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            EmployeeRep = new EmployeeRepository(context);
            ResponsibilityRep = new ResponsibilityRepository(context);
            ObjectiveRep = new ObjectiveRepository(context);
            CompanyRep = new CompanyRepository(context);
            DepartmentRep = new DepartmentRepository(context);
            UserRep = new UserRepository(context);

            CreateCompany();
            int workplace = ChooseWorkplace();
            employee = EnterWorkplace(workplace);

            int eid = AddEmployee();
            int oid = AddObjective();
            AddResponsibility(eid, oid);

            ClearDB();
        }

        private void CreateCompany()
        {
            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            rep.AddCompany("qoollo", 1994);

            var res1 = CompanyRep.GetAll().Last();
            Assert.That(res1.Title, Is.EqualTo("qoollo"), "AddCompany Title");
            Assert.That(res1.Foundationyear, Is.EqualTo(1994), "AddCompany Foundationyear");

            var tmp = EmployeeRep.GetAll();
            tmp.Sort((x, y) => x.Employeeid.CompareTo(y.Employeeid));
            var res2 = tmp.Last();
            Assert.That(res2.User_, Is.EqualTo("DarkBrandon"), "AddCompany User_");
            Assert.That(res2.Company, Is.EqualTo(res1.Companyid), "AddCompany Company");
            Assert.That(res2.Department, Is.EqualTo(null), "AddCompany Department");
            Assert.That(res2.Permission_, Is.EqualTo((int)Permissions.Founder), "AddCompany Permission_");
        }

        private int ChooseWorkplace()
        {
            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            List<WorkplaceView> res = rep.GetWorkplaces();

            Assert.That(res.Count, Is.EqualTo(2), "GetWorkplaces Count");
            Assert.That(res[1].Company.Companyid, Is.EqualTo(2), "GetWorkplaces Company");
            Assert.That(res[1].Department, Is.EqualTo(null), "GetWorkplaces Department");
            Assert.That(res[1].Permission_, Is.EqualTo(4), "GetWorkplaces Permission_");

            return res[1].EmployeeID;
        }

        private Employee EnterWorkplace(int workplace)
        {
            var rep = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            Employee res = rep.GetEmployeeByWorkplace(workplace);

            Assert.That(res.Employeeid, Is.EqualTo(workplace), "GetEmployeeByWorkplace Employee");
            Assert.That(res.User_, Is.EqualTo("DarkBrandon"), "GetEmployeeByWorkplace User_");

            return res;
        }

        private int AddEmployee()
        {
            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddEmployee("Daikatana", 1, null);

            var res = EmployeeRep.GetAll().Last();
            Assert.That(res.User_, Is.EqualTo("Daikatana"), "AddEmployee User_");
            Assert.That(res.Permission_, Is.EqualTo(1), "AddEmployee Permission_");
            Assert.That(res.Department, Is.EqualTo(null), "AddEmployee Department");

            return res.Employeeid;
        }

        private int AddObjective()
        {
            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddObjective(null, "important task", new DateTime(), new DateTime(), new TimeSpan());

            var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Parentobjective, Is.EqualTo(null), "AddObjective Parentobjective");
            Assert.That(res.Title, Is.EqualTo("important task"), "AddObjective Title");

            return res.Objectiveid;
        }

        private void AddResponsibility(int eid, int oid)
        {
            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.AddResponsible(eid, oid);

            var res = ResponsibilityRep.GetAll().Last();
            Assert.That(res.Employee, Is.EqualTo(eid), "AddResponsible Employee");
            Assert.That(res.Objective, Is.EqualTo(oid), "AddResponsible Objective");
        }

        private void ClearDB()
        {
            var rep = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            rep.DeleteCompany();
        }
    }
}

