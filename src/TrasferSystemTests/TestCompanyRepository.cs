//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using Microsoft.Data.Sqlite;

namespace TrasferSystemTests
{
    [TestFixture(Author = "mucha", Description = "Hehe")]
    [AllureNUnit]
    [AllureLink("localhost:80")]
    public class TestCompanyRepository
    {
        [Test]
        public void TestAdd() //in memory fix
        {
            var Company = new Company(_companyid: 2, _title: "Qoollo", _foundationyear: 1994);

            var options = new DbContextOptionsBuilder<transfersystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new transfersystemContext(options))
            {
                ICompanyRepository rep = new CompanyRepository(context);

                rep.Add(Company);

                Company checkCompany1 = rep.GetAll().Last();

                Assert.IsNotNull(checkCompany1, "Companys was not added");
                Assert.AreEqual("Qoollo", checkCompany1.Title, "Not equal Added Company");
                Assert.AreEqual(1994, checkCompany1.Foundationyear, "Not equal Added Company");

                rep.Delete(checkCompany1);
            }
        }

        [Test]
        public void TestGetAll()
        {

            var options = new DbContextOptionsBuilder<transfersystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new transfersystemContext(options))
            {
                var Company = new Company(_companyid: 2000, _title: "Qoollo", _foundationyear: 1994);

                ICompanyRepository rep = new CompanyRepository(context);
                rep.Add(Company);

                List<Company> Companys = rep.GetAll();

                Assert.IsNotNull(Companys, "Can't find Companys");
                Assert.AreEqual("Qoollo", Companys.Last().Title, "Not equal Added Company");
                Assert.AreEqual(1994, Companys.Last().Foundationyear, "Not equal Added Company");

                rep.Delete(Companys.Last());
            }     
        }

        [Test]
        public void TestUpdate()
        {
            var Company = new Company(_companyid: 2000, _title: "Qoollo", _foundationyear: 1994);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            ICompanyRepository rep = new CompanyRepository(context);
            rep.Add(Company);
            Company addedCompany = rep.GetAll().Last();

            Company newCompany = new Company(_companyid: addedCompany.Companyid, _title: "Qoollo", _foundationyear: 2001);
            
            rep.Update(newCompany);

            Company checkCompany2 = rep.GetCompanyByID(newCompany.Companyid);

            Assert.IsNotNull(checkCompany2, "cannot find Company by id");
            Assert.AreEqual("Qoollo", checkCompany2.Title, "Not equal Added Company");
            Assert.AreEqual(2001, checkCompany2.Foundationyear, "Not equal Added Company");

            rep.Delete(addedCompany);
        }

        [Test]
        public void TestDelete()
        {
            var Company = new Company(_companyid: 2000, _title: "Qoollo", _foundationyear: 1994);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            ICompanyRepository rep = new CompanyRepository(context);
            rep.Add(Company);
            Company addedCompany = rep.GetAll().Last();

            rep.Delete(addedCompany);

            Assert.IsNull(rep.GetCompanyByID(addedCompany.Companyid), "Company was not deleted");
        }

        [Test]
        public void TestGetCompanyByID()
        {
            var Company = new Company(_companyid: 2000, _title: "Qoollo", _foundationyear: 1994);
            var context = new transfersystemContext(Connection.GetConnection(Permissions.Founder.ToString()));

            ICompanyRepository rep = new CompanyRepository(context);
            rep.Add(Company);
            Company addedCompany = rep.GetAll().Last();

            Company checkCompany1 = rep.GetCompanyByID(addedCompany.Companyid);

            Assert.IsNotNull(checkCompany1, "Companys1 was not found");
            Assert.AreEqual("Qoollo", checkCompany1.Title, "Not equal found Company");
            Assert.AreEqual(1994, checkCompany1.Foundationyear, "Not equal found Company");

            rep.Delete(addedCompany);
        }
    }
}
