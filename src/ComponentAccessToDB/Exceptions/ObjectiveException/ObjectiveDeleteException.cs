using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class ObjectiveDeleteException : Exception
    {
        public ObjectiveDeleteException() : base() { }
        public ObjectiveDeleteException(string message) : base(message) { }
        public ObjectiveDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}

