//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TrasferSystemTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestEmployeeRepository
    {
        [Test]
        public void TestAdd()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);

            rep.Add(Employee);

            Employee checkEmployee1 = rep.GetAll().Last();

            Assert.IsNotNull(checkEmployee1, "Employees was not added");
            Assert.AreEqual("DarkBrandon", checkEmployee1.User_, "Not equal Added Employee");
            Assert.AreEqual(1, checkEmployee1.Company, "Not equal Added Employee");
            Assert.AreEqual(null, checkEmployee1.Department, "Not equal Added Employee");
            Assert.AreEqual(3, checkEmployee1.Permission_, "Not equal Added Employee");

            rep.Delete(checkEmployee1);
        }

        [Test]
        public void TestGetAll()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            
            List<Employee> Employees = rep.GetAll();

            Assert.IsNotNull(Employees, "Can't find Employees");
            Assert.AreEqual("DarkBrandon", Employees.Last().User_, "Not equal Added Employee");
            Assert.AreEqual(1, Employees.Last().Company, "Not equal Added Employee");
            Assert.AreEqual(null, Employees.Last().Department, "Not equal Added Employee");
            Assert.AreEqual(3, Employees.Last().Permission_, "Not equal Added Employee");

            rep.Delete(Employees.Last());
        }

        [Test]
        public void TestUpdate()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            Employee addedEmployee = rep.GetAll().Last();

            Employee newEmployee = new Employee(_employeeid: addedEmployee.Employeeid, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 1);

            rep.Update(newEmployee);

            Employee checkEmployee2 = rep.GetEmployeeByID(newEmployee.Employeeid);

            Assert.IsNotNull(checkEmployee2, "cannot find Employee by id");
            Assert.AreEqual("DarkBrandon", newEmployee.User_, "Not equal added Employee");
            Assert.AreEqual(1, checkEmployee2.Company, "Not equal Added Employee");
            Assert.AreEqual(null, checkEmployee2.Department, "Not equal Added Employee");
            Assert.AreEqual(1, checkEmployee2.Permission_, "Not equal Added Employee");

            rep.Delete(addedEmployee);
        }

        [Test]
        public void TestDelete()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            Employee addedEmployee = rep.GetAll().Last();

            rep.Delete(addedEmployee);

            Assert.IsNull(rep.GetEmployeeByID(addedEmployee.Employeeid), "Employee was not deleted");
        }

        [Test]
        public void TestGetEmployeeByID()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            Employee addedEmployee = rep.GetAll().Last();

            Employee checkEmployee1 = rep.GetEmployeeByID(addedEmployee.Employeeid);

            Assert.IsNotNull(checkEmployee1, "Employees1 was not found");
            Assert.AreEqual("DarkBrandon", checkEmployee1.User_, "Not equal found Employee");
            Assert.AreEqual(1, checkEmployee1.Company, "Not equal found Employee");
            Assert.AreEqual(null, checkEmployee1.Department, "Not equal found Employee");
            Assert.AreEqual(3, checkEmployee1.Permission_, "Not equal found Employee");

            rep.Delete(addedEmployee);
        }

        [Test]
        public void GetResponsibleEmployees()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: null, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            Employee addedEmployee = rep.GetAll().Last();

            IResponsibilityRepository rep1 = new ResponsibilityRepository(context);
            Responsibility Responsibility = new Responsibility(_responsibilityid: 1, _objective: 1, _employee: addedEmployee.Employeeid, _timespent: TimeSpan.Zero);
            rep1.Add(Responsibility);
            
            List<Employee> checkEmployee3 = rep.GetResponsibleEmployees(1);

            Assert.IsNotNull(checkEmployee3, "Can't find Employee by Responsibility");

            rep.Delete(addedEmployee);
        }

        [Test]
        public void TestGetEmployeeByWorkplace()
        {
            var Employee = new Employee(_employeeid: 2000, _user_: "DarkBrandon", _company: 1, _department: 1, _permission_: 3);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IEmployeeRepository rep = new EmployeeRepository(context);
            rep.Add(Employee);
            Employee addedEmployee = rep.GetAll().Last();

            Employee checkEmployee = rep.GetEmployeeByWorkplace("DarkBrandon", 1, 1);

            Assert.IsNotNull(checkEmployee, "Can't find Employees");
            Assert.AreEqual("DarkBrandon", checkEmployee.User_, "Not equal found Employee");
            Assert.AreEqual(1, checkEmployee.Company, "Not equal found Employee");
            Assert.AreEqual(1, checkEmployee.Department, "Not equal found Employee");
            Assert.AreEqual(3, checkEmployee.Permission_, "Not equal found Employee");

            rep.Delete(addedEmployee);
        }
    }
}
