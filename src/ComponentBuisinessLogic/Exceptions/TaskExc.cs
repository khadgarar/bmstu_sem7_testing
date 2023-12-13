using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class TaskException : Exception
    {

        public TaskException() : base() { }
        public TaskException(string message, Exception? innerException) 
            : base(message, innerException) { }

        public TaskException(string message) 
            : base(message + "Task")  {   }
    }
}
