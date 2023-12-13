using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class EmployeeDeleteException : Exception
    {
        public EmployeeDeleteException() : base() { }
        public EmployeeDeleteException(string message) : base(message) { }
        public EmployeeDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}