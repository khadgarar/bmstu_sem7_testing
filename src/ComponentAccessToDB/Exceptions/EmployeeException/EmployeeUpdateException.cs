using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class EmployeeUpdateException : Exception
    {
        public EmployeeUpdateException() : base() { }
        public EmployeeUpdateException(string message) : base(message) { }
        public EmployeeUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}