using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NodaTime;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class ObjectiveDB
    {
        public int Objectiveid { get; set; }
        public int? ParentTaskID { get; set; }
        public string Title { get; set; }
        public int CompanyID { get; set; }
        public int? DepartmentID { get; set; }
        public DateTime Termbegin { get; set; }
        public DateTime Termend { get; set; }
        public TimeSpan Estimatedtime { get; set; }
        
        public virtual ObjectiveDB ParentTask { get; set; }
        public virtual CompanyDB Company { get; set; }
        public virtual DepartmentDB Department { get; set; }
        public virtual ICollection<ObjectiveDB> SubTasks { get; set; }
        public virtual ICollection<ResponsibilityDB> Responsibilites { get; set; }
    }
}
