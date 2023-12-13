using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class CompanyUpdateException : Exception
    {
        public CompanyUpdateException() : base() { }
        public CompanyUpdateException(string message) : base(message) { }
        public CompanyUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}
