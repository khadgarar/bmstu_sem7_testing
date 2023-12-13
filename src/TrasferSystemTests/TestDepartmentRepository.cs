//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace TrasferSystemTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestDepartmentRepository
    {
        [Test]
        public void TestAdd()
        {
            var Department = new Department(_departmentid: 2000, _title: "Testing", _company: 1, _foundationyear: 1994, _activityfield: "Information tech");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);

            Department checkDepartment1 = rep.GetAll().Last();

            Assert.IsNotNull(checkDepartment1, "Departments was not added");
            Assert.AreEqual("Testing", checkDepartment1.Title, "Not equal Added Department");
            Assert.AreEqual(1, checkDepartment1.Company, "Not equal Added Department");
            Assert.AreEqual(1994, checkDepartment1.Foundationyear, "Not equal Added Department");
            Assert.AreEqual("Information tech", checkDepartment1.Activityfield, "Not equal Added Department");

            rep.Delete(checkDepartment1);
        }

        [Test]
        public void TestGetAll()
        {
            var Department = new Department(_departmentid: 2000, _title: "Testing", _company: 1, _foundationyear: 1994, _activityfield: "Information tech");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);
            
            List<Department> Departments = rep.GetAll();

            Assert.IsNotNull(Departments, "Can't find Departments");
            Assert.AreEqual("Testing", Departments.Last().Title, "Not equal Added Department");
            Assert.AreEqual(1, Departments.Last().Company, "Not equal Added Department");
            Assert.AreEqual(1994, Departments.Last().Foundationyear, "Not equal Added Department");
            Assert.AreEqual("Information tech", Departments.Last().Activityfield, "Not equal Added Department");

            rep.Delete(Departments.Last());
        }

        [Test]
        public void TestUpdate()
        {
            var Department = new Department(_departmentid: 2000, _title: "hello", _company: 1, _foundationyear: 1994, _activityfield: "no");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);
            Department addedDepartment = rep.GetAll().Last();

            Department newDepartment = new Department(_departmentid: addedDepartment.Departmentid, _title: "hello", _company: 1, _foundationyear: 1994, _activityfield: "Fix bug");

            rep.Update(newDepartment);

            Department checkDepartment2 = rep.GetDepartmentByID(newDepartment.Departmentid);

            Assert.IsNotNull(checkDepartment2, "cannot find Department by id");
            Assert.AreEqual("hello", newDepartment.Title, "Not equal added Department");
            Assert.AreEqual(1, checkDepartment2.Company, "Not equal Added Department");
            Assert.AreEqual(1994, checkDepartment2.Foundationyear, "Not equal Added Department");
            Assert.AreEqual("Fix bug", checkDepartment2.Activityfield, "Not equal Added Department");

            rep.Delete(addedDepartment);
        }

        [Test]
        public void TestDelete()
        {
            var Department = new Department(_departmentid: 2000, _title: "Testing", _company: 1, _foundationyear: 1994, _activityfield: "Information tech");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);
            Department addedDepartment = rep.GetAll().Last();

            rep.Delete(addedDepartment);

            Assert.IsNull(rep.GetDepartmentByID(addedDepartment.Departmentid), "Department was not deleted");
        }

        [Test]
        public void TestGetDepartmentByID()
        {
            var Department = new Department(_departmentid: 2000, _title: "Testing", _company: 1, _foundationyear: 1994, _activityfield: "Information tech");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);
            Department addedDepartment = rep.GetAll().Last();

            Department checkDepartment1 = rep.GetDepartmentByID(addedDepartment.Departmentid);

            Assert.IsNotNull(checkDepartment1, "Departments1 was not found");
            Assert.AreEqual("Testing", checkDepartment1.Title, "Not equal found Department");
            Assert.AreEqual(1, checkDepartment1.Company, "Not equal found Department");
            Assert.AreEqual(1994, checkDepartment1.Foundationyear, "Not equal found Department");
            Assert.AreEqual("Information tech", checkDepartment1.Activityfield, "Not equal found Department");

            rep.Delete(addedDepartment);
        }

        [Test]
        public void TestGetDepartmentsByCompany()
        {
            var Department = new Department(_departmentid: 2000, _title: "Testing", _company: 1, _foundationyear: 1994, _activityfield: "Information tech");
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IDepartmentRepository rep = new DepartmentRepository(context);
            rep.Add(Department);
            Department addedDepartment = rep.GetAll().Last();

            List<Department> checkDepartment = rep.GetDepartmentsByCompany(1);

            Assert.IsNotNull(checkDepartment, "Can't find Departments");
            Assert.AreEqual("Testing", checkDepartment.Last().Title, "Not equal found Department");
            Assert.AreEqual(1, checkDepartment.Last().Company, "Not equal found Department");
            Assert.AreEqual(1994, checkDepartment.Last().Foundationyear, "Not equal found Department");
            Assert.AreEqual("Information tech", checkDepartment.Last().Activityfield, "Not equal found Department");

            rep.Delete(addedDepartment);
        }
    }
}
