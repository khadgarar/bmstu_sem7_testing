using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComponentBuisinessLogic
{
    public interface IObjectiveRepository : CrudRepository<Objective>
    {
        Objective GetObjectiveByID(int? id);
        List<Objective> GetObjectivesByTitle(string title);
        List<Objective> GetSubObjectives(int tid);
    }
}
