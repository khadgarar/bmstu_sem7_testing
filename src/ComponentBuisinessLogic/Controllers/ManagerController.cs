using System;
using System.Collections.Generic;

namespace ComponentBuisinessLogic
{
    public class ManagerController : EmployeeController
    {
        public ManagerController(User User,
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
        public bool AddObjective(int? pid, string title, DateTime termBegin, DateTime termEnd, TimeSpan estimatedTime, int? department = -1)
        {
            if (department != -1)
            {
                if (_Employee.Department != null)
                    return false;
            }
            else
                department = _Employee.Department;

            if (pid != null)
            {
                var tmp = ObjectiveRepository.GetObjectiveByID(pid);

                if (!CheckWorkplace(tmp))
                    return false;
            }

            Objective Objective = new Objective(_objectiveid: 0,
                                 _parentobjective: pid,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _department: department,
                                 _termbegin: termBegin,
                                 _termend: termEnd,
                                 _estimatedtime: estimatedTime);
            ObjectiveRepository.Add(Objective);
            return true;
        }
        public bool UpdateObjective(int tid, string title, DateTime termBegin, DateTime termEnd, TimeSpan estimatedTime, int? department = -1)
        {
            if (department != -1 )
            {
                if (_Employee.Department != null)
                    return false;
            }
            else
                department = _Employee.Department;

            var tmp = ObjectiveRepository.GetObjectiveByID(tid);

            if (!CheckWorkplace(tmp))
                return false;

            Objective task = new Objective(_objectiveid: tid,
                                 _parentobjective: tmp.Parentobjective,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _department: department,
                                 _termbegin: termBegin,
                                 _termend: termEnd,
                                 _estimatedtime: estimatedTime);
            ObjectiveRepository.Update(task);
            return true;
        }
        public bool DeleteObjective(int id)
        {
            Objective o = ObjectiveRepository.GetObjectiveByID(id);

            if (!CheckWorkplace(o))
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
        public bool AddResponsible(int eid, int tid)
        {
            var tmpObjective = ObjectiveRepository.GetObjectiveByID(tid);
            var tmpEmployee = EmployeeRepository.GetEmployeeByID(eid);

            if (!CheckСonformance(tmpEmployee, tmpObjective))
                return false;

            Responsibility oldResponsibility = ResponsibilityRepository.GetResponsibilityByObjectiveAndEmployee(tid, eid);
            Responsibility newResponsibility;

            if (oldResponsibility == null)
            {
                newResponsibility = new Responsibility(_responsibilityid: 0,
                                             _employee: eid,
                                             _objective: tid,
                                             _timespent: TimeSpan.Zero);
                ResponsibilityRepository.Add(newResponsibility);
            }
            else
                return false;

            return true;
        }
        public bool DeleteResponsibility(int eid, int tid)
        {
            var tmpObjective = ObjectiveRepository.GetObjectiveByID(tid);
            var tmpEmployee = EmployeeRepository.GetEmployeeByID(eid);

            if (!CheckСonformance(tmpEmployee, tmpObjective))
                return false;

            Responsibility o = ResponsibilityRepository.GetResponsibilityByObjectiveAndEmployee(tid, eid);

            if (o == null)
                return false;

            ResponsibilityRepository.Delete(o);
            return true;
        }
        public List<Responsibility> GetResponsibilityByEmployee(int id)
        {
            var tmpEmployee = EmployeeRepository.GetEmployeeByID(id);

            if (!CheckWorkplace(tmpEmployee))
                return null;

            return ResponsibilityRepository.GetResponsibilityByEmployee(id);
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
        public bool AddDepartment(string title, int foundationyear, string activityfield)
        {
            if (_Employee.Department != null)
                return false;

            Department department = new Department(_departmentid: 0,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _foundationyear: foundationyear,
                                 _activityfield: activityfield);
            DepartmentRepository.Add(department);
            return true;
        }
        public bool UpdateDepartment(int id, string title, int foundationyear, string activityfield)
        {
            if (_Employee.Department != null && _Employee.Department != id)
                return false;

            Department department = new Department(_departmentid: id,
                                 _title: title,
                                 _company: _Employee.Company,
                                 _foundationyear: foundationyear,
                                 _activityfield: activityfield);
            DepartmentRepository.Update(department);
            return true;
        }
        public bool DeleteDepartment(int id)
        {
            if (_Employee.Department != null)
                return false;

            Department department = DepartmentRepository.GetDepartmentByID(id);

            if (department == null)
                return false;

            DepartmentRepository.Delete(department);
            return true;
        }

        public bool CheckEmployeeDepartment(int id)
        {
            return _Employee.Department == id;
        }
    }
}
