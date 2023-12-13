using System;
using System.Collections.Generic;

#nullable disable
namespace ComponentAccessToDB
{
    public class ResponsibilityDeleteException : Exception
    {
        public ResponsibilityDeleteException() : base() { }
        public ResponsibilityDeleteException(string message) : base(message) { }
        public ResponsibilityDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}