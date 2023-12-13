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
    public class TestObjectiveRepository
    {
        [Test]
        public void TestAdd()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);

            Objective checkObjective1 = rep.GetAll().Last();

            Assert.IsNotNull(checkObjective1, "Objectives was not added");
            Assert.AreEqual(null, checkObjective1.Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Make bugs", checkObjective1.Title, "Not equal Added Objective");
            Assert.AreEqual(1, checkObjective1.Company, "Not equal Added Objective");
            Assert.AreEqual(null, checkObjective1.Department, "Not equal Added Objective");

            rep.Delete(checkObjective1);
        }

        [Test]
        public void TestGetAll()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            
            List<Objective> Objectives = rep.GetAll();

            Assert.IsNotNull(Objectives, "Can't find Objectives");
            Assert.AreEqual(null, Objectives.Last().Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Make bugs", Objectives.Last().Title, "Not equal Added Objective");
            Assert.AreEqual(1, Objectives.Last().Company, "Not equal Added Objective");
            Assert.AreEqual(null, Objectives.Last().Department, "Not equal Added Objective");

            rep.Delete(Objectives.Last());
        }

        [Test]
        public void TestUpdate()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            Objective addedObjective = rep.GetAll().Last();

            Objective newObjective = new Objective(_objectiveid: addedObjective.Objectiveid, _title: "Delete bugs", _company: 1, _department: null);

            rep.Update(newObjective);

            Objective checkObjective2 = rep.GetObjectiveByID(newObjective.Objectiveid);

            Assert.IsNotNull(checkObjective2, "cannot find Objective by id");
            Assert.AreEqual(null, checkObjective2.Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Delete bugs", newObjective.Title, "Not equal added Objective");
            Assert.AreEqual(1, checkObjective2.Company, "Not equal Added Objective");
            Assert.AreEqual(null, checkObjective2.Department, "Not equal Added Objective");

            rep.Delete(addedObjective);
        }

        [Test]
        public void TestDelete()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            Objective addedObjective = rep.GetAll().Last();

            rep.Delete(addedObjective);

            Assert.IsNull(rep.GetObjectiveByID(addedObjective.Objectiveid), "Objective was not deleted");
        }

        [Test]
        public void TestGetObjectiveByID()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            Objective addedObjective = rep.GetAll().Last();

            Objective checkObjective1 = rep.GetObjectiveByID(addedObjective.Objectiveid);

            Assert.IsNotNull(checkObjective1, "Objectives1 was not found");
            Assert.AreEqual(null, checkObjective1.Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Make bugs", checkObjective1.Title, "Not equal found Objective");
            Assert.AreEqual(1, checkObjective1.Company, "Not equal found Objective");
            Assert.AreEqual(null, checkObjective1.Department, "Not equal found Objective");

            rep.Delete(addedObjective);
        }

        [Test]
        public void TestGetSubObjectives()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: 1, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            Objective addedObjective = rep.GetAll().Last();

            List<Objective> checkObjective = rep.GetSubObjectives(1);

            Assert.IsNotNull(checkObjective, "Can't find Objectives");
            Assert.AreEqual(1, checkObjective.Last().Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Make bugs", checkObjective.Last().Title, "Not equal found Objective");
            Assert.AreEqual(1, checkObjective.Last().Company, "Not equal found Objective");
            Assert.AreEqual(null, checkObjective.Last().Department, "Not equal found Objective");

            rep.Delete(addedObjective);
        }

        [Test]
        public void TestGetObjectivesByTitle()
        {
            var Objective = new Objective(_objectiveid: 2000, _parentobjective: null, _title: "Make bugs", _company: 1, _department: null);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            IObjectiveRepository rep = new ObjectiveRepository(context);
            rep.Add(Objective);
            Objective addedObjective = rep.GetAll().Last();

            List<Objective> checkObjective = rep.GetObjectivesByTitle("Make bugs");

            Assert.IsNotNull(checkObjective, "Can't find Objectives");
            Assert.AreEqual(null, checkObjective.Last().Parentobjective, "Not equal found Objective");
            Assert.AreEqual("Make bugs", checkObjective.Last().Title, "Not equal found Objective");
            Assert.AreEqual(1, checkObjective.Last().Company, "Not equal found Objective");
            Assert.AreEqual(null, checkObjective.Last().Department, "Not equal found Objective");

            rep.Delete(addedObjective);
        }
    }
}
