namespace ComponentBuisinessLogic
{
    public class FounderController : ManagerController
    {
        public FounderController(User User,
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
        public bool AddEmployee(string user_, int permission_, int? department = -1)
        {
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
        public bool UpdateCompany(string title, int foundationyear)
        {
            Company employee = new Company(_companyid: _Employee.Company,
                                 _title: title,
                                 _foundationyear: foundationyear);
            CompanyRepository.Update(employee);
            return true;
        }
        public bool DeleteCompany()
        {
            Company company = CompanyRepository.GetCompanyByID(_Employee.Company);

            if (company == null)
                return false;

            CompanyRepository.Delete(company);
            return true;
        }
    }
}
