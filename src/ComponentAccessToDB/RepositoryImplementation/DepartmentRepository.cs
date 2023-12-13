using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBuisinessLogic;

namespace ComponentAccessToDB
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public DepartmentRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(Department element)
        {
            DepartmentDB t = DepartmentConv.BltoDB(element);

            if (db.Departments.Count() > 0)
                t.Departmentid = db.Departments.Max(comparer => comparer.Departmentid) + 1;
            else
                t.Departmentid = 1;

            try
            {
                db.Departments.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DepartmentAddException("DepartmentAdd", ex);
            }
        }
        public List<Department> GetAll()
        {
            IQueryable<DepartmentDB> Departments = db.Departments;
            List<DepartmentDB> conv = Departments.ToList();
            List<Department> final = new List<Department>();
            foreach (var m in conv)
            {
                final.Add(DepartmentConv.DBtoBL(m));
            }
            final.Sort((x, y) => x.Departmentid.CompareTo(y.Departmentid));
            return final;
        }
        public void Update(Department element)
        {
            DepartmentDB o = db.Departments.Find(element.Departmentid);

            if (o == null)
                return;

            o.Title = element.Title;
            o.CompanyID = element.Company;
            o.Foundationyear = element.Foundationyear;
            o.Activityfield = element.Activityfield;
            try
            {
                db.Departments.Update(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DepartmentUpdateException("DepartmentUpdate", ex);
            }
        }
        public void Delete(Department element)
        {
            DepartmentDB o = db.Departments.Find(element.Departmentid);
            if (o == null)
                return;

            try
            {
                db.Departments.Remove(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new DepartmentDeleteException("DepartmentDelete", ex);
            }
        }
        public Department GetDepartmentByID(int? departmentid)
        {
            if (departmentid == null)
                return null;

            DepartmentDB e = db.Departments.Find(departmentid);
            return e != null ? DepartmentConv.DBtoBL(e) : null;
        }
        public List<Department> GetDepartmentsByCompany(int company)
        {

            IQueryable<DepartmentDB> Departments = db.Departments.Where(needed => needed.CompanyID == company);
            List<DepartmentDB> conv = Departments.ToList();
            List<Department> final = new List<Department>();
            foreach (var m in conv)
            {
                final.Add(DepartmentConv.DBtoBL(m));
            }
            return final;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
