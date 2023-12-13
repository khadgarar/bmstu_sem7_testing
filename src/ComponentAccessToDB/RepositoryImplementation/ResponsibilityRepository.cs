using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBuisinessLogic;

namespace ComponentAccessToDB
{
    public class ResponsibilityRepository : IResponsibilityRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public ResponsibilityRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(Responsibility element)
        {
            ResponsibilityDB t = ResponsibilityConv.BltoDB(element);

            if (db.Responsibilities.Count() > 0)
                t.Responsibilityid = db.Responsibilities.Max(comparer => comparer.Responsibilityid) + 1;
            else
                t.Responsibilityid = 1;

            try
            {
                db.Responsibilities.Add(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ResponsibilityAddException("ResponsibilityAdd", ex);
            }
        }
        public List<Responsibility> GetAll()
        {
            IQueryable<ResponsibilityDB> Responsibilities = db.Responsibilities;
            List<ResponsibilityDB> conv = Responsibilities.ToList();
            List<Responsibility> final = new List<Responsibility>();
            foreach (var m in conv)
            {
                final.Add(ResponsibilityConv.DBtoBL(m));
            }
            final.Sort((x, y) => x.Responsibilityid.CompareTo(y.Responsibilityid));
            return final;
        }
        public void Update(Responsibility element)
        {
            ResponsibilityDB t = db.Responsibilities.Find(element.Responsibilityid);
            t.EmployeeID = element.Employee;
            t.ObjectiveID = element.Objective;
            t.Timespent = element.Timespent;
            try
            {
                db.Responsibilities.Update(t);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ResponsibilityUpdateException("ResponsibilityUpdate", ex);
            }
        }
        public void Delete(Responsibility element)
        {
            ResponsibilityDB t = ResponsibilityConv.BltoDB(element);
            if (t == null)
                return;

            try
            {
                db.Responsibilities.Remove(db.Responsibilities.Find(t.Responsibilityid));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ResponsibilityDeleteException("ResponsibilityDelete", ex);
            }
        }
        public List<Responsibility> GetResponsibilityByEmployee(int eid)
        {
            IQueryable<ResponsibilityDB> Responsibilities = db.Responsibilities.Where(needed => needed.EmployeeID == eid);
            List<ResponsibilityDB> conv = Responsibilities.ToList();
            List<Responsibility> final = new List<Responsibility>();
            foreach (var m in conv)
            {
                final.Add(ResponsibilityConv.DBtoBL(m));
            }
            return final;
        }
        public Responsibility GetResponsibilityByObjectiveAndEmployee(int tid, int eid)
        {
            IQueryable<ResponsibilityDB> Responsibilities = db.Responsibilities.Where(needed => needed.ObjectiveID == tid && needed.EmployeeID == eid);
            List<ResponsibilityDB> conv = Responsibilities.ToList();
            List<Responsibility> final = new List<Responsibility>();
            foreach (var m in conv)
            {
                final.Add(ResponsibilityConv.DBtoBL(m));
            }
            return final.Count() > 0  ? final.First() : null;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
