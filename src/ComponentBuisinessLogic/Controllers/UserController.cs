using System.Collections.Generic;
using System.Linq;

namespace ComponentBuisinessLogic
{
    public class UserController
    {
        protected IUserRepository UserRepository;
        protected ICompanyRepository CompanyRepository;
        protected IDepartmentRepository DepartmentRepository;
        protected IEmployeeRepository EmployeeRepository;
        protected User _User;
        public UserController(User User,
                              IUserRepository UserRep,
                              ICompanyRepository CompanyRep,
                              IDepartmentRepository DepartmentRep,
                              IEmployeeRepository EmployeeRep)
        {
            EmployeeRepository = EmployeeRep;
            UserRepository = UserRep;
            CompanyRepository = CompanyRep;
            DepartmentRepository = DepartmentRep;
            _User = User;
        }
        public UserView GetUser()
        {
            UserView userview = new UserView(_login: _User.Login,
                                        _name_: _User.Name_,
                                        _surname: _User.Surname);
            return userview;
        }
        public bool AddCompany(string title, int foundationyear)
        {
            Company company = new Company(_companyid: 0,
                                 _title: title,
                                 _foundationyear: foundationyear);
            CompanyRepository.Add(company);

            Employee employee = new Employee(_employeeid: 0,
                                 _user_: _User.Login,
                                 _company: CompanyRepository.GetAll().Last().Companyid,
                                 _department: null,
                                 _permission_: (int)Permissions.Founder);
            EmployeeRepository.Add(employee);

            return true;
        }
        public List<WorkplaceView> GetWorkplaces()
        {
            var employees = EmployeeRepository.GetAll().Where(el => el.User_ == _User.Login).ToList();
            List<WorkplaceView> final = new List<WorkplaceView>();
            foreach (var m in employees)
            {
                var company = CompanyRepository.GetCompanyByID(m.Company);
                var department = DepartmentRepository.GetDepartmentByID(m.Department);
                final.Add(new WorkplaceView
                (
                    _EmployeeID: m.Employeeid,
                    _company: company,
                    _department: department,
                    _permission_: m.Permission_
                ));
            }
            return final;
        }
        public Employee GetEmployeeByWorkplace(int id)
        {
            return EmployeeRepository.GetEmployeeByID(id);
        }
        public bool UpdateUser(string password_, string name_, string surname)
        {
            User user = new User(_login: _User.Login,
                                 _password_: password_,
                                 _name_: name_,
                                 _surname: surname);
            UserRepository.Update(user);
            return true;
        }
        public bool DeleteUser()
        {
            User user = UserRepository.GetUserByLogin(_User.Login);

            if (user == null)
                return false;

            UserRepository.Delete(user);
            return true;
        }
    }
}
