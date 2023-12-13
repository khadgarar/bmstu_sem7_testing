using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentAccessToDB
{
    public class UserUpdateException : Exception
    {
        public UserUpdateException() : base() { }
        public UserUpdateException(string message) : base(message) { }
        public UserUpdateException(string message, Exception inner) : base(message, inner) { }
    }
}
