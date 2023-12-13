using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class UserAddException : Exception
    {
        public UserAddException() : base() { }
        public UserAddException(string message) : base(message) { }
        public UserAddException(string message, Exception inner) : base(message, inner) { }
    }
}
