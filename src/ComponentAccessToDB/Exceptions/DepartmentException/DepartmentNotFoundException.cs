using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException() : base() { }
        public DepartmentNotFoundException(string message) : base(message) { }
        public DepartmentNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
