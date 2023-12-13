using System;
using System.Collections.Generic;

#nullable disable
namespace ComponentAccessToDB
{
    public class CompanyDeleteException : Exception
    {
        public CompanyDeleteException() : base() { }
        public CompanyDeleteException(string message) : base(message) { }
        public CompanyDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}
