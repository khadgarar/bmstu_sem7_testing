using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException() : base() { }
        public CompanyNotFoundException(string message) : base(message) { }
        public CompanyNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
