using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
