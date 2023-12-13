using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class CompanyConv
    {
        public static CompanyDB BltoDB(Company a_bl)
        {
            return new CompanyDB
            {
                Companyid = a_bl.Companyid,
                Title = a_bl.Title,
                Foundationyear = a_bl.Foundationyear
            };
        }

        public static Company DBtoBL(CompanyDB a_bl)
        {
            return new Company
            (
                _companyid: a_bl.Companyid,
                _title: a_bl.Title,
                _foundationyear: a_bl.Foundationyear
            );
        }
    }
}
