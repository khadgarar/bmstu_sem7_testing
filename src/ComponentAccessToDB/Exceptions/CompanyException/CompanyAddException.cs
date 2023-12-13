using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class CompanyAddException : Exception
    {
        public CompanyAddException() : base() { }
        public CompanyAddException(string message) : base(message) { }
        public CompanyAddException(string message, Exception inner) : base(message, inner) { }
    }
}
