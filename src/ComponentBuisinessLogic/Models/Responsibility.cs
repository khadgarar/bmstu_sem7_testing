using System;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class Responsibility 
    {
        public Responsibility(
            int _responsibilityid = 1,
            int _employee = 1,
            int _objective = 1,
            TimeSpan _timespent = new TimeSpan())
        {
            Responsibilityid = _responsibilityid;
            Employee = _employee;
            Objective = _objective;
            Timespent = _timespent;
        }

        public int Responsibilityid { get; set; }
        public int Employee { get; set; }
        public int Objective { get; set; }
        public TimeSpan Timespent { get; set; }
    }
}
