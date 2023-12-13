using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class DepartmentDB
    {
        public int Departmentid { get; set; }
        public string Title { get; set; }
        public int CompanyID { get; set; }
        public int Foundationyear { get; set; }
        public string Activityfield { get; set; }

        public virtual CompanyDB Company { get; set; }
        public virtual ICollection<EmployeeDB> Employees { get; set; }
        public virtual ICollection<ObjectiveDB> Objectives { get; set; }
    }
}
