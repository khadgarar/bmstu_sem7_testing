using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class EmployeeException : Exception
    {

        public EmployeeException() : base() { }
        public EmployeeException(string message, Exception? innerException) 
            : base(message, innerException) { }

        public EmployeeException(string message) 
            : base(message + "Employee") { }
    }
}
