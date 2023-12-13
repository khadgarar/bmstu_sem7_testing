using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class CompanyDB
    {
        public int Companyid { get; set; }
        public string Title { get; set; }
        public int Foundationyear { get; set; }

        public virtual ICollection<DepartmentDB> Departments { get; set; }
        public virtual ICollection<EmployeeDB> Employees { get; set; }
        public virtual ICollection<ObjectiveDB> Objectives { get; set; }
    }
}
