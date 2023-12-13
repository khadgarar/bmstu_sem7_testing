#nullable disable

namespace ComponentBuisinessLogic
{
    public class Employee
    {
        public Employee(int _employeeid = 1, 
            string _user_ = "", 
            int _company = 1, 
            int? _department = null, 
            int _permission_ = 0)
        {
            Employeeid = _employeeid;
            User_ = _user_;
            Company = _company;
            Department = _department;
            Permission_ = _permission_;
        }

        public int Employeeid { get; }
        public string User_ { get; }
        public int Company { get; }
        public int? Department { get; }
        public int Permission_ { get; }
    }
}
