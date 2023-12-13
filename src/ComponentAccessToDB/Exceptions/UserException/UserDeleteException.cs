using System;
using System.Collections.Generic;

#nullable disable
namespace ComponentAccessToDB
{
    public class UserDeleteException : Exception
    {
        public UserDeleteException() : base() { }
        public UserDeleteException(string message) : base(message) { }
        public UserDeleteException(string message, Exception inner) : base(message, inner) { }
    }
}
