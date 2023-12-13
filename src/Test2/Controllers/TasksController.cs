using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Test2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private EmployeeController _employee;
        private ResponsibleController _responsible;
        private ManagerController _manager;
        private HRController _HR;
        private transfersystemContext db;

        public TasksController()
        {
            var _config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("dbappsettings.json")
                   .Build();
            db = new transfersystemContext(Connection.GetConnection((int)Permissions.Manager, _config));

            Employee user = new Employee();
            IEmployeeRepository employeeRep = new EmployeeRepository(db);
            IObjectiveRepository objectiveRep = new ObjectiveRepository(db);
            ITimeSpentRepository timeSpentRep = new TimeSpentRepository(db);
            IEmployeeDepartmentRepository employeeDepartmentRep = new EmployeeDepartmentRepository(db);

            _employee = new EmployeeController(user, employeeRep, timeSpentRep, objectiveRep);
            _responsible = new ResponsibleController(user, employeeRep, timeSpentRep, objectiveRep);
            _manager = new ManagerController(user, employeeRep, timeSpentRep, objectiveRep);
            _HR = new HRController(user, employeeRep, timeSpentRep, objectiveRep, employeeDepartmentRep);
        }

        [HttpGet]
        public IActionResult Get([FromHeader] string Autharization)
        {
            return Ok(Autharization);
            //return Ok(_manager.GetAllTasks());
        }

        [HttpPost]
        public IActionResult Post(Objective value)
        {
            var res = _manager.AddProject(value.Title, value.TermBegin, value.TermEnd, value.EstimatedTime);

            if (!res)
                return BadRequest();
            return Ok();
        }
    }
}
