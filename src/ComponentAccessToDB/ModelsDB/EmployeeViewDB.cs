using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ComponentAccessToDB
{
    [Keyless]
    public partial class EmployeeViewDB
    {
        public int Employeeid { get; set; }
        public string Login { get; set; }
        public string Name_ { get; set; }
        public string Surname { get; set; }
        public int? Department { get; set; }
        public int Permission_ { get; }
    }
}
