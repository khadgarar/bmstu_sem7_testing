using System.Collections.Generic;
using System.Linq;

namespace ComponentBuisinessLogic
{
    public enum Permissions : int
    {
        Employee,
        Responsible,
        Manager,
        HR,
        Founder,
        Notauth,
        User,
    }
    public class EmployeeController : UserController
    {
        protected IObjectiveRepository ObjectiveRepository;
        protected IResponsibilityRepository ResponsibilityRepository;
        protected Employee _Employee;
        public EmployeeController(User User,
                              Employee Employee,
                              IUserRepository UserRep,
                              ICompanyRepository CompanyRep,
                              IDepartmentRepository DepartmentRep,
                              IEmployeeRepository EmployeeRep,
                              IObjectiveRepository ObjectiveRep,
                              IResponsibilityRepository ResponsibilityRep) :
            base(User, UserRep, CompanyRep, DepartmentRep, EmployeeRep)
        {
            ObjectiveRepository = ObjectiveRep;
            ResponsibilityRepository = ResponsibilityRep;
            _Employee = Employee;
        }
        protected bool CheckWorkplace(Employee el)
        {
            if (el == null)
                return false;

            return el.Company == _Employee.Company &&
                   (_Employee.Department == null || el.Department == _Employee.Department);
        }
        protected bool CheckWorkplace(Objective el)
        {
            if (el == null)
                return false;

            return el.Company == _Employee.Company &&
                   (_Employee.Department == null || el.Department == _Employee.Department);
        }
        protected List<Employee> GetWorkplaceEmployees(List<Employee> employes)
        {
            return employes.Where(el => CheckWorkplace(el)).ToList();
        }
        protected List<Objective> GetWorkplaceObjectives(List<Objective> objectives)
        {
            return objectives.Where(el => CheckWorkplace(el)).ToList();
        }
        public List<Department> GetAllDepartments()
        {
            if (_Employee.Department != null)
                return null;

            return DepartmentRepository.GetDepartmentsByCompany(_Employee.Company);
        }
        public List<EmployeeView> GetAllEmployees()
        {
            List<Employee> tmpemployees = EmployeeRepository.GetAll();
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
        public List<Objective> GetAllObjectives()
        {
            List<Objective> AllObjectives = ObjectiveRepository.GetAll().ToList();
            return GetWorkplaceObjectives(AllObjectives);
        }
        public List<Objective> GetObjectiveByID(int tid)
        {
            Objective o = ObjectiveRepository.GetObjectiveByID(tid);
            if (o == null)
                return null;

            List<Objective> final = new List<Objective>();
            final.Add(o);
            final.AddRange(ObjectiveRepository.GetSubObjectives(tid));
            return GetWorkplaceObjectives(final);
        }
        public List<Objective> GetObjectivesByTitle(string title)
        {
            List<Objective> AllObjectives = ObjectiveRepository.GetObjectivesByTitle(title);
            return GetWorkplaceObjectives(AllObjectives);
        }
        public WorkplaceView GetWorkplace()
        {
            var company = CompanyRepository.GetCompanyByID(_Employee.Company);
            var department = DepartmentRepository.GetDepartmentByID(_Employee.Department);
            WorkplaceView final = new WorkplaceView
            (
                _EmployeeID: _Employee.Employeeid,
                _company: company,
                _department: department,
                _permission_: _Employee.Permission_
            );
            return final;
        }
    }
}
