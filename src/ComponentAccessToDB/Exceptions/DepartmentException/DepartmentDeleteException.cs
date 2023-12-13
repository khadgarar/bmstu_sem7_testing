using System;
using System.Collections.Generic;

#nullable disable
namespace ComponentAccessToDB
{
    public class DepartmentDeleteException : Exception
    {
        public DepartmentDeleteException() : base() { }
        public DepartmentDeleteException(string message) : base(message) { }
        public DepartmentDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}
