using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBuisinessLogic
{
    public interface IResponsibilityRepository : CrudRepository<Responsibility>
    {
        List<Responsibility> GetResponsibilityByEmployee(int eid);
        Responsibility GetResponsibilityByObjectiveAndEmployee(int tid, int eid);
    }
}
