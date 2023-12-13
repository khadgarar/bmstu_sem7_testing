using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class DepartmentUpdateException : Exception
    {
        public DepartmentUpdateException() : base() { }
        public DepartmentUpdateException(string message) : base(message) { }
        public DepartmentUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}
