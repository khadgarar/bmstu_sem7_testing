using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class EmployeeView
    {
        public EmployeeView(int _employeeid = 0,
            string _login = "",
            string _name_ = "",
            string _surname = null,
            int? _department = null,
            int _permission_ = 0)
        {
            Employeeid = _employeeid;
            Login = _login;
            Name_ = _name_;
            Surname = _surname;
            Department = _department;
            Permission_ = _permission_;
        }

        public int Employeeid { get; set; }
        public string Login { get; set; }
        public string Name_ { get; set; }
        public string Surname { get; set; }
        public int? Department { get; set; }
        public int Permission_ { get; }
    }
}
