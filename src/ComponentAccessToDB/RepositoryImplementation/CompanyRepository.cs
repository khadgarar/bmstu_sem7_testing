using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBuisinessLogic;

namespace ComponentAccessToDB
{
    public class CompanyRepository : ICompanyRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public CompanyRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(Company element)
        {
            CompanyDB t = CompanyConv.BltoDB(element);

            if (db.Companies.Count() > 0)
                t.Companyid = db.Companies.Max(comparer => comparer.Companyid) + 1;
            else
                t.Companyid = 1;

            try
            {
                db.Companies.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CompanyAddException("CompanyAdd", ex);
            }
        }
        public List<Company> GetAll()
        {
            IQueryable<CompanyDB> Companys = db.Companies;
            List<CompanyDB> conv = Companys.ToList();
            List<Company> final = new List<Company>();
            foreach (var m in conv)
            {
                final.Add(CompanyConv.DBtoBL(m));
            }
            final.Sort((x, y) => x.Companyid.CompareTo(y.Companyid));
            return final;
        }
        public void Update(Company element)
        {
            CompanyDB o = db.Companies.Find(element.Companyid);
            o.Title = element.Title;
            o.Foundationyear = element.Foundationyear;
            try
            {
                db.Companies.Update(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CompanyUpdateException("CompanyUpdate", ex);
            }
        }
        public void Delete(Company element)
        {
            CompanyDB o = db.Companies.Find(element.Companyid);
            if (o == null)
                return;

            try
            {
                db.Companies.Remove(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CompanyDeleteException("CompanyDelete", ex);
            }
        }
        public Company GetCompanyByID(int companyid)
        {
            CompanyDB e = db.Companies.Find(companyid);
            return e != null ? CompanyConv.DBtoBL(e) : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
