using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class ResponsibilityDB
    {
        public int Responsibilityid { get; set; }
        public int EmployeeID { get; set; }
        public int ObjectiveID { get; set; }
        public TimeSpan Timespent { get; set; }

        public virtual EmployeeDB Employee { get; set; }
        public virtual ObjectiveDB Objective { get; set; }
    }
}
