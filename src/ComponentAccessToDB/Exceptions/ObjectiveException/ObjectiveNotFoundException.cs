using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ObjectiveNotFoundException : Exception
    {
        public ObjectiveNotFoundException() : base() { }
        public ObjectiveNotFoundException(string message) : base(message) { }
        public ObjectiveNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}

