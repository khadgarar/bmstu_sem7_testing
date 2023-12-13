using System.Collections.Generic;

namespace ComponentBuisinessLogic
{
    public class HRController : EmployeeController
    {
        public HRController(User User,
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
                    _login: m.User_,
                    _name_: user.Name_,
                    _surname: user.Surname,
                    _department: m.Department,
                    _permission_: m.Permission_
                ));
            }
            return final;
        }
        public bool AddEmployee(string user_, int permission_, int? department = -1)
        {
            if (user_ == _User.Login)
                return false;

            if (permission_ == (int)Permissions.Founder)
                return false;

            if (department != -1)
            {
                if (_Employee.Department != null)
                    return false;
            }
            else
                department = _Employee.Department;

            Employee employee = new Employee(_employeeid: 0,
                                 _user_: user_,
                                 _company: _Employee.Company,
                                 _department: department,
                                 _permission_: permission_);
            EmployeeRepository.Add(employee);
            return true;
        }
        public bool UpdateEmployee(int id, string user_, int permission_, int? department = -1)
        {
            if (id == _Employee.Employeeid)
                return false;

            if (permission_ == (int)Permissions.Founder)
                return false;

            if (department != -1)
            {
                if (_Employee.Department != null)
                    return false;
            }
            else
                department = _Employee.Department;

            var tmpEmployee = EmployeeRepository.GetEmployeeByID(id);

            if (!CheckWorkplace(tmpEmployee))
                return false;

            Employee employee = new Employee(_employeeid: id,
                                 _user_: user_,
                                 _company: _Employee.Company,
                                 _department: department,
                                 _permission_: permission_);
            EmployeeRepository.Update(employee);
            return true;
        }
        public bool DeleteEmployee(int id)
        {
            Employee employee = EmployeeRepository.GetEmployeeByID(id);

            if (!CheckWorkplace(employee))
                return false;

            if (employee == null)
                return false;

            EmployeeRepository.Delete(employee);
            return true;
        }
    }
}
