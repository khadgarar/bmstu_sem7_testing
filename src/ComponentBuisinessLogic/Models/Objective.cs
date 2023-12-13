using System;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class Objective
    {
        public Objective(int _objectiveid = 1,
            int? _parentobjective = null, 
            string _title = "", 
            int _company = 1,
            int? _department = null,
            DateTime _termbegin = new DateTime(),
            DateTime _termend = new DateTime(),
            TimeSpan _estimatedtime = new TimeSpan())
        {
            Objectiveid = _objectiveid;
            Parentobjective = _parentobjective;
            Title = _title;
            Company = _company;
            Department = _department;
            Termbegin = _termbegin;
            Termend = _termend;
            Estimatedtime = _estimatedtime;
        }

        public int Objectiveid { get; set; }
        public int? Parentobjective { get; set; }
        public string Title { get; set; }
        public int Company { get; set; }
        public int? Department { get; set; }
        public DateTime Termbegin { get; set; }
        public DateTime Termend { get; set; }
        public TimeSpan Estimatedtime { get; set; }
    }
}
