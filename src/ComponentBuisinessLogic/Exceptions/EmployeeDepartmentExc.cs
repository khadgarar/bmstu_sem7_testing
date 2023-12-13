using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class EmployeeDepartmentException : Exception
    {

        public EmployeeDepartmentException() : base() { }
        public EmployeeDepartmentException(string message, Exception? innerException) 
            : base(message, innerException) { }

        public EmployeeDepartmentException(string message) 
            : base(message + "EmployeeDepartment") { }
    }
}
