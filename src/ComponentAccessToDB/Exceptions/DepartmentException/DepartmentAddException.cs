using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class DepartmentAddException : Exception
    {
        public DepartmentAddException() : base() { }
        public DepartmentAddException(string message) : base(message) { }
        public DepartmentAddException(string message, Exception inner) : base(message, inner) { }
    }
}
