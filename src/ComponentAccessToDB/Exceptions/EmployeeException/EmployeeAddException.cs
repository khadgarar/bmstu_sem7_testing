using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class EmployeeAddException : Exception
    {
        public EmployeeAddException() : base() { }
        public EmployeeAddException(string message) : base(message) { }
        public EmployeeAddException(string message, Exception inner) : base(message, inner) { }
    }
}

