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

            //employeecheck = new Employee(1, "DarkBrandon", )
            var companytoadd = new Company(2, "qoollo", 1994);
            var workplacetoadd = new WorkplaceView(6, companytoadd, null, 4);
            var employeetoadd = new Employee(7, "Daikatana", 1, null, 1);
            var objectivetoadd = new Objective(2, null, "important task", 2, null, new DateTime(), new DateTime(), new TimeSpan());

            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            EmployeeRep = new EmployeeRepository(context);
            ResponsibilityRep = new ResponsibilityRepository(context);
            ObjectiveRep = new ObjectiveRepository(context);
            CompanyRep = new CompanyRepository(context);
            DepartmentRep = new DepartmentRepository(context);
            UserRep = new UserRepository(context);

            //CreateCompany();
            AddAndEnterCompany(companytoadd, workplacetoadd);
            employee = EnterWorkplace(workplacetoadd);

            AddEmployee(employeetoadd);
            AddObjective(objectivetoadd);
            AddResponsibility(employeetoadd.Employeeid, objectivetoadd.Objectiveid);

            ClearDB();
        }

        private void AddAndEnterCompany(Company companytoadd, WorkplaceView workplacetoadd)
        {
            var controller = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            controller.AddCompany(companytoadd.Title, companytoadd.Foundationyear);

            List<WorkplaceView> res = controller.GetWorkplaces();

            Assert.That(res.Count, Is.EqualTo(2), "GetWorkplaces Count");
            Assert.That(res[1].Company.Companyid, Is.EqualTo(workplacetoadd.Company.Companyid), "GetWorkplaces Company");
            Assert.That(res[1].Department, Is.EqualTo(workplacetoadd.Department), "GetWorkplaces Department");
            Assert.That(res[1].Permission_, Is.EqualTo(workplacetoadd.Permission_), "GetWorkplaces Permission_");
        }

        private Employee EnterWorkplace(WorkplaceView workplace)
        {
            var controller = new UserController(
                user, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep);

            Employee res = controller.GetEmployeeByWorkplace(workplace.EmployeeID);

            Assert.That(res.Employeeid, Is.EqualTo(workplace.EmployeeID), "GetEmployeeByWorkplace Employee");
            Assert.That(res.User_, Is.EqualTo("DarkBrandon"), "GetEmployeeByWorkplace User_");

            return res;
        }

        private void AddEmployee(Employee employeetoadd)
        {
            var controller = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            controller.AddEmployee(employeetoadd.User_, employeetoadd.Permission_, employeetoadd.Department);

            var res = controller.GetAllEmployees().Last();
            Assert.That(res.Login, Is.EqualTo(employeetoadd.User_), "AddEmployee User_");
            Assert.That(res.Permission_, Is.EqualTo(employeetoadd.Permission_), "AddEmployee Permission_");
            Assert.That(res.Department, Is.EqualTo(employeetoadd.Department), "AddEmployee Department");

        }

        private int AddObjective(Objective objectivetoadd)
        {
            var controller = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            controller.AddObjective(objectivetoadd.Parentobjective, objectivetoadd.Title, objectivetoadd.Termbegin, objectivetoadd.Termend, objectivetoadd.Estimatedtime);
            var res = controller.GetAllObjectives().Last();
            //var res = ObjectiveRep.GetAll().Last();
            Assert.That(res.Parentobjective, Is.EqualTo(objectivetoadd.Parentobjective), "AddObjective Parentobjective");
            Assert.That(res.Title, Is.EqualTo(objectivetoadd.Title), "AddObjective Title");

            return res.Objectiveid;
        }

        private void AddResponsibility(int eid, int oid)
        {
            var controller = new FounderController(
                user, employee, UserRep,
                CompanyRep, DepartmentRep, EmployeeRep,
                ObjectiveRep, ResponsibilityRep);

            controller.AddResponsible(eid, oid);
            var res = controller.GetAllResponsibilities().Last();           
            //var res = ResponsibilityRep.GetAll().Last();
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

