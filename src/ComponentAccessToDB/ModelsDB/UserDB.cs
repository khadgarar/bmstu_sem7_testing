using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class UserDB
    {
        public string Login { get; set; }
        public string Password_ { get; set; }
        public string Name_ { get; set; }
        public string Surname { get; set; }

        public virtual ICollection<EmployeeDB> Employees { get; set; }
    }
}
