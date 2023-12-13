using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBuisinessLogic
{
    public interface IEmployeeRepository : CrudRepository<Employee>
    {
        Employee GetEmployeeByID(int id);
        List<Employee> GetResponsibleEmployees(int tid);
        Employee GetEmployeeByWorkplace(string user, int cid, int? did);
        List<EmployeeView> TimeTestModels(int tid, int company, int? department);
        List<EmployeeView> TimeTestSQL(int tid, int company, int? department);
    }
}
