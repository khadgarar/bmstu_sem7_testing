using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ComponentBuisinessLogic
{
    public class ResponsibleController : EmployeeController
    {
        public ResponsibleController(User User,
                              Employee Employee,
                              IUserRepository UserRep,
                              ICompanyRepository CompanyRep,
                              IDepartmentRepository DepartmentRep,
                              IEmployeeRepository EmployeeRep,
                              IObjectiveRepository ObjectiveRep,
                              IResponsibilityRepository ResponsibilityRep) :
            base(User, Employee, UserRep, CompanyRep, DepartmentRep, EmployeeRep, ObjectiveRep, ResponsibilityRep)
        {
        }
        private bool CheckResponsibility(int tid)
        {
            bool flag = false;
            var o = ObjectiveRepository.GetObjectiveByID(tid);

            if (o == null)
                return flag;

            List<Employee> responsibles = EmployeeRepository.GetResponsibleEmployees(o.Objectiveid);
            foreach (var resp in responsibles)
                if (resp.User_ == _Employee.User_)
                    return true;

            while (o.Parentobjective != null)
            {
                o = ObjectiveRepository.GetObjectiveByID(o.Parentobjective);

                responsibles = EmployeeRepository.GetResponsibleEmployees(o.Objectiveid);
                foreach (var resp in responsibles)
                    if (resp.User_ == _Employee.User_)
                        return true;
            }

            return flag;
        }
        public bool AddSubObjective(int pid, string title, DateTime termBegin, DateTime termEnd, TimeSpan estimatedTime)
        {
            var tmp = ObjectiveRepository.GetObjectiveByID(pid);

            if (!(CheckWorkplace(tmp) && CheckResponsibility(pid)))
                return false;

            Objective Objective = new Objective(_objectiveid: 0,
                                 _parentobjective: pid,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _department: _Employee.Department,
                                 _termbegin: termBegin,
                                 _termend: termEnd,
                                 _estimatedtime: estimatedTime);
            ObjectiveRepository.Add(Objective);
            return true;
        }
        public bool UpdateObjective(int tid, string title, DateTime termBegin, DateTime termEnd, TimeSpan estimatedTime)
        {
            var tmp = ObjectiveRepository.GetObjectiveByID(tid);

            if (!(CheckWorkplace(tmp) && CheckResponsibility(tid)))
                return false;

            Objective Objective = new Objective(_objectiveid: tid,
                                 _parentobjective: tmp.Parentobjective,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _department: _Employee.Department,
                                 _termbegin: termBegin,
                                 _termend: termEnd,
                                 _estimatedtime: estimatedTime);
            ObjectiveRepository.Update(Objective);
            return true;
        }
        public bool DeleteSubObjective(int id)
        {
            Objective o = ObjectiveRepository.GetObjectiveByID(id);

            if (!(CheckWorkplace(o) && CheckResponsibility(id)))
                return false;

            if (o.Parentobjective == null)
                return false;

            if (o == null)
                return false;

            ObjectiveRepository.Delete(o);
            return true;
        }
        private bool CheckСonformance(Employee emp, Objective obj)
        {
            return CheckWorkplace(emp) && CheckWorkplace(obj) &&
                   (emp.Department == null || emp.Department == obj.Department);
        }
        public List<EmployeeView> GetResponsibleEmployees(int id)
        {
            List<Employee> tmpemployees = EmployeeRepository.GetResponsibleEmployees(id);
            List<Employee> employees = GetWorkplaceEmployees(tmpemployees);
            List<EmployeeView> final = new List<EmployeeView>();
            foreach (var m in employees)
            {
                var user = UserRepository.GetUserByLogin(m.User_);
                final.Add(new EmployeeView
                (
                    _employeeid: m.Employeeid,
                    _login: m.User_,
                    _name_: user.Name_,
                    _surname: user.Surname,
                    _department: m.Department,
                    _permission_: m.Permission_
                ));
            }
            return final;
        }
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public List<EmployeeView> TimeTest(int id)
        {
            int n = 1000;

            List<EmployeeView> final = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                final = EmployeeRepository.TimeTestModels(id, _Employee.Company, _Employee.Department);
            stopwatch.Stop();
            var modelsTime = stopwatch.ElapsedMilliseconds / n;

            final = null;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < n; i++)
                final = EmployeeRepository.TimeTestSQL(id, _Employee.Company, _Employee.Department);
            stopwatch.Stop();

            Console.WriteLine("Time in ms for models implementation: " + modelsTime);
            Console.WriteLine("Time in ms for SQL implementation: " + stopwatch.ElapsedMilliseconds / n);
            return final;
        }
        public bool AddResponsibility(int eid, int tid, TimeSpan timeAmount)
        {
            var tmpObjective = ObjectiveRepository.GetObjectiveByID(tid);
            var tmpEmployee = EmployeeRepository.GetEmployeeByID(eid);

            if (!(CheckСonformance(tmpEmployee, tmpObjective) && CheckResponsibility(tid)))
                return false;

            Responsibility oldResponsibility = ResponsibilityRepository.GetResponsibilityByObjectiveAndEmployee(tid, eid);
            Responsibility newResponsibility;

            if (oldResponsibility == null)
            {
                newResponsibility = new Responsibility(_responsibilityid: 0,
                                             _employee: eid,
                                             _objective: tid,
                                             _timespent: timeAmount);
                ResponsibilityRepository.Add(newResponsibility);
            }
            else
            {
                newResponsibility = new Responsibility(_responsibilityid: oldResponsibility.Responsibilityid,
                                             _employee: eid,
                                             _objective: tid,
                                             _timespent: timeAmount);
                ResponsibilityRepository.Update(newResponsibility);
            }

            return true;
        }
        public bool DeleteResponsibility(int eid, int tid)
        {
            var tmpObjective = ObjectiveRepository.GetObjectiveByID(tid);
            var tmpEmployee = EmployeeRepository.GetEmployeeByID(eid);

            if (!(CheckСonformance(tmpEmployee, tmpObjective) && CheckResponsibility(tid)))
                return false;

            Responsibility o = ResponsibilityRepository.GetResponsibilityByObjectiveAndEmployee(tid, eid);

            if (o == null)
                return false;

            ResponsibilityRepository.Delete(o);
            return true;
        }
    }
}
