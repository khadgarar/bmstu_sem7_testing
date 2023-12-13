using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ResponsibilityNotFoundException : Exception
    {
        public ResponsibilityNotFoundException() : base() { }
        public ResponsibilityNotFoundException(string message) : base(message) { }
        public ResponsibilityNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}