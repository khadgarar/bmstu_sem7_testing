using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ResponsibilityUpdateException : Exception
    {
        public ResponsibilityUpdateException() : base() { }
        public ResponsibilityUpdateException(string message) : base(message) { }
        public ResponsibilityUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}