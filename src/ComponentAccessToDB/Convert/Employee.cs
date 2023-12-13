using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class EmployeeConv
    {
        public static EmployeeDB BltoDB(Employee a_bl)
        {
            return new EmployeeDB
            {
                Employeeid = a_bl.Employeeid,
                UserID = a_bl.User_,
                CompanyID = a_bl.Company,
                DepartmentID = a_bl.Department,
                Permission_ = a_bl.Permission_
            };
        }

        public static Employee DBtoBL(EmployeeDB a_bl)
        {
            return new Employee
            (
                _employeeid: a_bl.Employeeid,
                _user_: a_bl.UserID,
                _company: a_bl.CompanyID,
                _department: a_bl.DepartmentID,
                _permission_: a_bl.Permission_
            );
        }
    }
}
