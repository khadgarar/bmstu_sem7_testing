using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class EmployeeDB
    {
        public int Employeeid { get; set; }
        public string UserID { get; set; }
        public int CompanyID { get; set; }
        public int? DepartmentID { get; set; }
        public int Permission_ { get; set; }

        public virtual UserDB User { get; set; }
        public virtual CompanyDB Company { get; set; }
        public virtual DepartmentDB Department { get; set; }
        public virtual ICollection<ResponsibilityDB> Responsibilites { get; set; }
    }
}
