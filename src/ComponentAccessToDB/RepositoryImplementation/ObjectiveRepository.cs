using System;
using System.Collections.Generic;
using System.Linq;
using ComponentBuisinessLogic;

namespace ComponentAccessToDB
{
    public class ObjectiveRepository : IObjectiveRepository, IDisposable
    {
        private readonly transfersystemContext db;
        public ObjectiveRepository(transfersystemContext curDb)
        {
            db = curDb;
        }
        public void Add(Objective element)
        {
            ObjectiveDB o = ObjectiveConv.BltoDB(element);

            if (db.Objectives.Count() > 0)
                o.Objectiveid = db.Objectives.Max(comparer => comparer.Objectiveid) + 1;
            else
                o.Objectiveid = 1;

            try
            {
                db.Objectives.Add(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ObjectiveAddException("ObjectiveAdd", ex);
            }
        }
        public List<Objective> GetAll()
        {
            IQueryable<ObjectiveDB> Objectives = db.Objectives;
            List<ObjectiveDB> conv = Objectives.ToList();
            List<Objective> final = new List<Objective>();
            foreach (var m in conv)
            {
                final.Add(ObjectiveConv.DBtoBL(m));
            }
            final.Sort((x, y) => x.Objectiveid.CompareTo(y.Objectiveid));
            return final;
        }
        public void Update(Objective element)
        {
            ObjectiveDB o = db.Objectives.Find(element.Objectiveid);
            o.ParentTaskID = element.Parentobjective;
            o.Title = element.Title;
            o.CompanyID = element.Company;
            o.DepartmentID = element.Department;
            o.Termbegin = element.Termbegin;
            o.Termend = element.Termend;
            o.Estimatedtime = element.Estimatedtime;
            try
            {
                db.Objectives.Update(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ObjectiveUpdateException("ObjectiveUpdate", ex);
            }
        }
        public void Delete(Objective element)
        {
            ObjectiveDB o = db.Objectives.Find(element.Objectiveid);
            if (o == null)
                return;

            try
            {
                db.Objectives.Remove(o);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ObjectiveDeleteException("ObjectiveDelete", ex);
            }
        }
        public Objective GetObjectiveByID(int? id)
        {
            ObjectiveDB oDB = db.Objectives.Find(id);
            if (oDB == null)
                return null;

            Objective o = ObjectiveConv.DBtoBL(oDB);
            return o;
        }
        public List<Objective> GetObjectivesByTitle(string title)
        {
            IQueryable<ObjectiveDB> Objectives = db.Objectives.Where(needed => needed.Title == title);
            List<ObjectiveDB> conv = Objectives.ToList();
            List<Objective> final = new List<Objective>();
            foreach (var m in conv)
            {
                final.Add(ObjectiveConv.DBtoBL(m));
            }
            return final;
        }
        public List<Objective> GetSubObjectives(int tid)
        {
            IQueryable<ObjectiveDB> Objectives = db.Objectives.Where(needed => needed.ParentTaskID == tid);
            List<ObjectiveDB> conv = Objectives.ToList();
            List<Objective> final = new List<Objective>();
            foreach (var m in conv)
            {
                final.Add(ObjectiveConv.DBtoBL(m));
            }
            return final;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
