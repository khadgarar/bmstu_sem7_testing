using System;
using System.Collections.Generic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class Objective
    {
        public int ParentTaskId { get; set; }
        public string Title { get; set; }
        public string TermBegin { get; set; }
        public string TermEnd { get; set; }
        public string EstimatedTime { get; set; }
    }
}
