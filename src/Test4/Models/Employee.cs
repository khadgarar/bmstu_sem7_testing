using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class EmployeeUI
    {
        public int EmployeeID { get; set; }
        public string User_ { get; set; }
        public int Company { get; set; }
        public int? Department { get; set; }
        public int Permission_ { get; set; }
    }
}
