using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ResponsibilityAddException : Exception
    {
        public ResponsibilityAddException() : base() { }
        public ResponsibilityAddException(string message) : base(message) { }
        public ResponsibilityAddException(string message, Exception inner) : base(message, inner) { }
    }
}