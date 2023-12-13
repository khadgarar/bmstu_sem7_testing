using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class TimeSpentException : Exception
    {

        public TimeSpentException() : base() { }
        public TimeSpentException(string message, Exception? innerException) 
            : base(message, innerException) { }

        public TimeSpentException(string message) 
            : base(message + "TimeSpent") { }
    }
}
