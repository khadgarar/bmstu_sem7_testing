//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using System;

namespace TrasferSystemTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestResponsibilityRepository
    {
        [Test]
        public void TestAdd()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));
            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            
            rep.Add(Responsibility);

            Responsibility checkResponsibility1 = rep.GetAll().Last();

            Assert.IsNotNull(checkResponsibility1, "Responsibilitys was not added");
            Assert.AreEqual(1, checkResponsibility1.Employee, "Not equal Added Responsibility");
            Assert.AreEqual(1, checkResponsibility1.Objective, "Not equal Added Responsibility");

            rep.Delete(checkResponsibility1);
        }

        [Test]
        public void TestGetAll()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            rep.Add(Responsibility);
            
            List<Responsibility> Responsibilitys = rep.GetAll();

            Assert.IsNotNull(Responsibilitys, "Can't find Responsibilitys");
            Assert.AreEqual(1, Responsibilitys.Last().Employee, "Not equal Added Responsibility");
            Assert.AreEqual(1, Responsibilitys.Last().Objective, "Not equal Added Responsibility");

            rep.Delete(Responsibilitys.Last());
        }

        [Test]
        public void TestUpdate()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            rep.Add(Responsibility);
            Responsibility addedResponsibility = rep.GetAll().Last();

            Responsibility newResponsibility = new Responsibility(_responsibilityid: addedResponsibility.Responsibilityid, _employee: 1, _objective: 1, _timespent: TimeSpan.FromSeconds(1));

            rep.Update(newResponsibility);

            Responsibility checkResponsibility2 = rep.GetResponsibilityByObjectiveAndEmployee(1, 1);

            Assert.IsNotNull(checkResponsibility2, "cannot find Responsibility by id");
            Assert.AreEqual(1, checkResponsibility2.Employee, "Not equal Added Responsibility");
            Assert.AreEqual(1, checkResponsibility2.Objective, "Not equal Added Responsibility");
            Assert.AreEqual(TimeSpan.FromSeconds(1), checkResponsibility2.Timespent, "Not equal Added Responsibility");

            rep.Delete(addedResponsibility);
        }

        [Test]
        public void TestDelete()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            rep.Add(Responsibility);
            Responsibility addedResponsibility = rep.GetAll().Last();

            rep.Delete(addedResponsibility);

            Assert.IsNull(rep.GetResponsibilityByObjectiveAndEmployee(1, 1), "Responsibility was not deleted");
        }

        [Test]
        public void TestGetResponsibilityByObjectiveAndEmployee()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            rep.Add(Responsibility);
            Responsibility addedResponsibility = rep.GetAll().Last();

            Responsibility checkResponsibility1 = rep.GetResponsibilityByObjectiveAndEmployee(1, 1);

            Assert.IsNotNull(checkResponsibility1, "Responsibilitys1 was not found");
            Assert.AreEqual(1, checkResponsibility1.Employee, "Not equal found Responsibility");
            Assert.AreEqual(1, checkResponsibility1.Objective, "Not equal found Responsibility");
            Assert.AreEqual(new TimeSpan(), checkResponsibility1.Timespent, "Not equal found Responsibility");

            rep.Delete(addedResponsibility);
        }

        [Test]
        public void TestGetResponsibilityByEmployee()
        {
            var Responsibility = new Responsibility(_responsibilityid: 2000, _employee: 1, _objective: 1, _timespent: new TimeSpan());
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IResponsibilityRepository rep = new ResponsibilityRepository(context);
            rep.Add(Responsibility);
            Responsibility addedResponsibility = rep.GetAll().Last();

            List<Responsibility> checkResponsibility = rep.GetResponsibilityByEmployee(1);

            Assert.IsNotNull(checkResponsibility, "Can't find Responsibilitys");
            Assert.AreEqual(1, checkResponsibility.Last().Employee, "Not equal found Responsibility");
            Assert.AreEqual(1, checkResponsibility.Last().Objective, "Not equal found Responsibility");
            Assert.AreEqual(new TimeSpan(), checkResponsibility.Last().Timespent, "Not equal found Responsibility");

            rep.Delete(addedResponsibility);
        }
    }
}
