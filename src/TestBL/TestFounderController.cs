using ComponentBuisinessLogic;
using Moq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TestBL
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestFounderController
    {
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

            var rep = new FounderController(
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

            var rep = new FounderController(
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

            var rep = new FounderController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.DeleteEmployee(2);

            EmployeeRep.Verify(x => x.Delete(It.Is<Employee>(x =>
                x.Employeeid == 2)),
                Times.Once);
        }

        [Test]
        public void TestUpdateCompany()
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

            var rep = new FounderController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.UpdateCompany("heh", 0);

            CompanyRep.Verify(x => x.Update(It.Is<Company>(x =>
                x.Companyid == 1 && x.Title == "heh" && x.Foundationyear == 0)),
                Times.Once);
        }

        [Test]
        public void TestDeleteCompany()
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
                .Returns(new Company(1));

            var rep = new FounderController(
                user, employee, UserRep.Object,
                CompanyRep.Object, DepartmentRep.Object, EmployeeRep.Object,
                ObjectiveRep.Object, ResponsibilityRep.Object);

            rep.DeleteCompany();

            CompanyRep.Verify(x => x.Delete(It.Is<Company>(x =>
                x.Companyid == 1)),
                Times.Once);
        }
    }
}

