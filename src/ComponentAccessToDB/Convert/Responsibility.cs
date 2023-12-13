using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class ResponsibilityConv
    {
        public static ResponsibilityDB BltoDB(Responsibility a_bl)
        {
            return new ResponsibilityDB
            {
                Responsibilityid = a_bl.Responsibilityid,
                EmployeeID = a_bl.Employee,
                ObjectiveID = a_bl.Objective,
                Timespent = a_bl.Timespent
            };
        }

        public static Responsibility DBtoBL(ResponsibilityDB a_bl)
        {
            return new Responsibility
            (
                _responsibilityid: a_bl.Responsibilityid,
                _employee: a_bl.EmployeeID,
                _objective: a_bl.ObjectiveID,
                _timespent: a_bl.Timespent
            );
        }
    }
}
