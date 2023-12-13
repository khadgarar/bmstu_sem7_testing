using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComponentBuisinessLogic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Runtime.CompilerServices;

namespace ComponentAccessToDB
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public EmployeeRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(Employee element)
        {
            EmployeeDB e = EmployeeConv.BltoDB(element);

            if (db.Employees.Count() > 0)
                e.Employeeid = db.Employees.Max(comparer => comparer.Employeeid) + 1;
            else
                e.Employeeid = 1;

            try
            {
                db.Employees.Add(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EmployeeAddException("EmployeeAdd", ex);
            }
        }
        public List<Employee> GetAll()
        {
            IQueryable<EmployeeDB> Employees = db.Employees;
            List<EmployeeDB> conv = Employees.ToList();
            List<Employee> final = new List<Employee>();
            foreach (var m in conv)
            {
                final.Add(EmployeeConv.DBtoBL(m));
            }
            final.Sort((x, y) => x.Employeeid.CompareTo(y.Employeeid));
            return final;
        }
        public void Update(Employee element)
        {
            EmployeeDB e = db.Employees.Find(element.Employeeid);
            e.UserID = element.User_;
            e.CompanyID = element.Company;
            e.DepartmentID = element.Department;
            e.Permission_ = element.Permission_;
            try
            {
                db.Employees.Update(e);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EmployeeUpdateException("EmployeeUpdate", ex);
            }
        }
        public void Delete(Employee element)
        {
            EmployeeDB e = EmployeeConv.BltoDB(element);
            if (e == null)
                return;

            try
            {
                db.Employees.Remove(db.Employees.Find(e.Employeeid));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new EmployeeDeleteException("EmployeeDelete", ex);
            }
        }
        public Employee GetEmployeeByID(int id)
        {
            EmployeeDB e = db.Employees.Find(id);
            return e != null ? EmployeeConv.DBtoBL(e) : null;
        }
        public List<Employee> GetResponsibleEmployees(int tid)
        {
            IQueryable<ResponsibilityDB> Responsibilities = db.Responsibilities.Where(needed => needed.ObjectiveID == tid);
            List<ResponsibilityDB> tmp = Responsibilities.ToList();
            List<EmployeeDB> conv = new List<EmployeeDB>();
            foreach (var m in tmp)
            {
                IQueryable<EmployeeDB> Employees = db.Employees.Where(needed => needed.Employeeid == m.EmployeeID);
                conv.AddRange(Employees.ToList());
            }
            List<Employee> final = new List<Employee>();
            foreach (var m in conv)
            {
                final.Add(EmployeeConv.DBtoBL(m));
            }
            return final;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public List<EmployeeView> TimeTestModels(int tid, int company, int? department)
        {
            IQueryable<ResponsibilityDB> Responsibilities = db.Responsibilities.Where(needed => needed.ObjectiveID == tid);
            List<ResponsibilityDB> tmp = Responsibilities.ToList();
            List<EmployeeDB> conv = new List<EmployeeDB>();
            foreach (var m in tmp)
            {
                IQueryable<EmployeeDB> Employees = db.Employees.Where(needed =>
                    needed.Employeeid == m.EmployeeID &&
                    needed.CompanyID == company &&
                    (department == null || needed.DepartmentID == department));

                conv.AddRange(Employees.ToList());
            }
            List<EmployeeView> final = new List<EmployeeView>();
            foreach (var m in conv)
            {
                var user = db.Users.Find(m.UserID);
                final.Add(new EmployeeView
                (
                    _employeeid: m.Employeeid,
                    _login: m.UserID,
                    _name_: user.Name_,
                    _surname: user.Surname,
                    _department: m.DepartmentID,
                    _permission_: m.Permission_
                ));
            }
            return final;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public List<EmployeeView> TimeTestSQL(int tid, int company, int? department)
        {
            List<EmployeeViewDB> tmp;
            if (department == null)
                tmp = db.EmployeeViews.FromSqlRaw("select ev.employeeid, ev.login, ev.name_, ev.surname, ev.department, ev.permission_ from employeeview ev join employee e on ev.employeeid = e.employeeid join responsibility r on r.employee = ev.employeeid where r.objective = {0} and e.company = {1} and (null is null or e.department = null)", tid, company).ToList();
            else
                tmp = db.EmployeeViews.FromSqlRaw("select ev.employeeid, ev.login, ev.name_, ev.surname, ev.department, ev.permission_ from employeeview ev join employee e on ev.employeeid = e.employeeid join responsibility r on r.employee = ev.employeeid where r.objective = {0} and e.company = {1} and ({2} is null or e.department = {2})", tid, company, (int)department).ToList();
            List<EmployeeView> final = new List<EmployeeView>();
            foreach (var m in tmp)
            {
                final.Add(new EmployeeView
                (
                    _employeeid: m.Employeeid,
                    _login: m.Login,
                    _name_: m.Name_,
                    _surname: m.Surname,
                    _department: m.Department,
                    _permission_: m.Permission_
                ));
            }
            return final;
        }
        public Employee GetEmployeeByWorkplace(string user, int cid, int? did)
        {
            IQueryable<EmployeeDB> Employees = db.Employees.Where(needed => needed.UserID == user && needed.CompanyID == cid && needed.DepartmentID == did); ;
            List<EmployeeDB> conv = Employees.ToList();
            List<Employee> final = new List<Employee>();
            foreach (var m in conv)
            {
                final.Add(EmployeeConv.DBtoBL(m));
            }
            return final.Count() > 0 ? final.First() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
