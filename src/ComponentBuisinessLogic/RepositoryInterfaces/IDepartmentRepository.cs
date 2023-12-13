using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBuisinessLogic
{
    public interface IDepartmentRepository : CrudRepository<Department>
    {
        Department GetDepartmentByID(int? departmentid);
        List<Department> GetDepartmentsByCompany(int company);
    }
}
