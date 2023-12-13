using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class WorkplaceView
    {
        public WorkplaceView(int _EmployeeID = 0, Company _company = null, Department _department = null, int _permission_ = 0)
        {
            EmployeeID = _EmployeeID;
            Company = _company;
            Department = _department;
            Permission_ = _permission_;
        }

        public int EmployeeID { get; }
        public Company Company { get; }
        public Department Department { get; }
        public int Permission_ { get; }
    }
}
