using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class DepartmentConv
    {
        public static DepartmentDB BltoDB(Department a_bl)
        {
            return new DepartmentDB
            {
                Departmentid = a_bl.Departmentid,
                Title = a_bl.Title,
                CompanyID = a_bl.Company,
                Foundationyear = a_bl.Foundationyear,
                Activityfield = a_bl.Activityfield
            };
        }

        public static Department DBtoBL(DepartmentDB a_bl)
        {
            return new Department
            (
                _departmentid: a_bl.Departmentid,
                _title: a_bl.Title,
                _company: a_bl.CompanyID,
                _foundationyear: a_bl.Foundationyear,
                _activityfield: a_bl.Activityfield
            );
        }
    }
}
