using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ObjectiveAddException : Exception
    {
        public ObjectiveAddException() : base() { }
        public ObjectiveAddException(string message) : base(message) { }
        public ObjectiveAddException(string message, Exception inner) : base(message, inner) { }
    }
}

