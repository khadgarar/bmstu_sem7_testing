using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBuisinessLogic
{
    public interface ICompanyRepository : CrudRepository<Company>
    {
        Company GetCompanyByID(int companyid);
    }
}
