using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base() { }
        public EmployeeNotFoundException(string message) : base(message) { }
        public EmployeeNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}