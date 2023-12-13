using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ObjectiveUpdateException : Exception
    {
        public ObjectiveUpdateException() : base() { }
        public ObjectiveUpdateException(string message) : base(message) { }
        public ObjectiveUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}

