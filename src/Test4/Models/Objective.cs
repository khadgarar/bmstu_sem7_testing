using NodaTime;
using System;
using System.Collections.Generic;

#nullable disable

namespace Test4.Models
{
    public class ObjectiveUI
    {
        public int? Parentobjective { get; set; }
        public string Title { get; set; }
        public int? Department { get; set; }
        public DateTime Termbegin { get; set; }
        public DateTime Termend { get; set; }
        public TimeSpan Estimatedtime { get; set; }
    }
}
