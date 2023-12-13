using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class ObjectiveConv
    {
        public static ObjectiveDB BltoDB(Objective a_bl)
        {
            return new ObjectiveDB
            {
                Objectiveid = a_bl.Objectiveid,
                ParentTaskID = a_bl.Parentobjective,
                Title = a_bl.Title,
                CompanyID = a_bl.Company,
                DepartmentID = a_bl.Department,
                Termbegin = a_bl.Termbegin,
                Termend = a_bl.Termend,
                Estimatedtime = a_bl.Estimatedtime
            };
        }

        public static Objective DBtoBL(ObjectiveDB a_bl)
        {
            return new Objective
            (
                _objectiveid: a_bl.Objectiveid,
                _parentobjective: a_bl.ParentTaskID,
                _title: a_bl.Title,
                _company: a_bl.CompanyID,
                _department: a_bl.DepartmentID,
                _termbegin: a_bl.Termbegin,
                _termend: a_bl.Termend,
                _estimatedtime: a_bl.Estimatedtime
            );
        }
    }
}
