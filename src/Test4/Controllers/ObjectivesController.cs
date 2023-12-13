using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Test4.Models;

namespace Test4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ObjectivesController : BaseController
    {
        private readonly transfersystemContext db;

        private IUserRepository userRep;
        private ICompanyRepository companyRep;
        private IDepartmentRepository departmentRep;
        private IEmployeeRepository employeeRep;
        private IObjectiveRepository objectiveRep;
        private IResponsibilityRepository responsibilityRep;

        User user;
        Employee employee;
        EmployeeController _employee;
        ResponsibleController _responsible;
        ManagerController _manager;
        HRController _HR;
        FounderController _founder;

        public ObjectivesController(transfersystemContext dbContext)
        {
            db = dbContext;

            userRep = new UserRepository(db);
            companyRep = new CompanyRepository(db);
            departmentRep = new DepartmentRepository(db);
            employeeRep = new EmployeeRepository(db);
            objectiveRep = new ObjectiveRepository(db);
            responsibilityRep = new ResponsibilityRepository(db);
        }

        private void ControllersInit()
        {
            user = userRep.GetUserByLogin(Username);

            var identity = User.Identity as ClaimsIdentity;
            int eid = int.Parse(identity.FindFirst("EmployeeID").Value);
            employee = employeeRep.GetEmployeeByID(eid);

            _employee = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            _responsible = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            _manager = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            _HR = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            _founder = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
        }

        [HttpGet]
        [Authorize(Roles = "Employee, Manager, Responsible, HR, Founder")]
        public IActionResult GetProjects()
        {
            ControllersInit();

            List<Objective> res;
            if (User.IsInRole("Employee"))
                res = _employee.GetAllObjectives();
            else if (User.IsInRole("Responsible"))
                res = _responsible.GetAllObjectives();
            else if (User.IsInRole("Manager"))
                res = _manager.GetAllObjectives();
            else if (User.IsInRole("HR"))
                res = _HR.GetAllObjectives();
            else
                res = _founder.GetAllObjectives();

            return Ok(res);
        }

        [HttpGet("/objectives/search/{title}")]
        [Authorize(Roles = "Employee, Manager, Responsible, HR, Founder")]
        public IActionResult GetObjectiveByTitle(string title)
        {
            ControllersInit();

            List<Objective> res;
            if (User.IsInRole("Employee"))
                res = _employee.GetObjectivesByTitle(title);
            else if (User.IsInRole("Responsible"))
                res = _responsible.GetObjectivesByTitle(title);
            else if (User.IsInRole("Manager"))
                res = _manager.GetObjectivesByTitle(title);
            else if (User.IsInRole("HR"))
                res = _HR.GetObjectivesByTitle(title);
            else
                res = _founder.GetObjectivesByTitle(title);

            return Ok(res);
        }

        [HttpGet("/objectives/{objectiveID:int}")]
        [Authorize(Roles = "Employee, Manager, Responsible, HR, Founder")]
        public IActionResult GetObjectiveByID(int objectiveID)
        {
            ControllersInit();

            List<Objective> res;
            if (User.IsInRole("Employee"))
                res = _employee.GetObjectiveByID(objectiveID);
            else if (User.IsInRole("Responsible"))
                res = _responsible.GetObjectiveByID(objectiveID);
            else if (User.IsInRole("Manager"))
                res = _manager.GetObjectiveByID(objectiveID);
            else if (User.IsInRole("HR"))
                res = _HR.GetObjectiveByID(objectiveID);
            else
                res = _founder.GetObjectiveByID(objectiveID);

            return Ok(res);
        }

        [HttpPost("/objectives/{objectiveID:int}")]
        [Authorize(Roles = "Responsible, Manager, Founder")]
        public IActionResult PostSubtask(int objectiveID, ObjectiveUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.AddObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);
            else if (User.IsInRole("Founder"))
                res = _founder.AddObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);
            else
                res = _responsible.AddSubObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime);

            if (!res)
                return BadRequest();
            
            return Ok();
        }

        [HttpPut("/objectives/{objectiveID:int}")]
        [Authorize(Roles = "Responsible, Manager, Founder")]
        public IActionResult Put(int objectiveID, ObjectiveUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.UpdateObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);
            else if (User.IsInRole("Founder"))
                res = _founder.UpdateObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);
            else
                res = _responsible.UpdateObjective(objectiveID, value.Title, value.Termbegin, value.Termend, value.Estimatedtime);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("/objectives/{objectiveID:int}")]
        [Authorize(Roles = "Responsible, Manager, Founder")]
        public IActionResult DeleteSubtask(int objectiveID)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.DeleteObjective(objectiveID);
            else if (User.IsInRole("Founder"))
                res = _founder.DeleteObjective(objectiveID);
            else
                res = _responsible.DeleteSubObjective(objectiveID);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Founder")]
        public IActionResult PostProject(ObjectiveUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.AddObjective(null, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);
            else
                res = _founder.AddObjective(null, value.Title, value.Termbegin, value.Termend, value.Estimatedtime, value.Department);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpGet("/objectives/{objectiveID:int}/responsible")]
        [Authorize(Roles = "Manager, Responsible, HR, Founder")]
        public IActionResult GetResponsibleEmployees(int objectiveID)
        {
            ControllersInit();

            List<EmployeeView> res;
            if (User.IsInRole("Responsible"))
                res = _responsible.GetResponsibleEmployees(objectiveID);
            else if (User.IsInRole("Manager"))
                res = _manager.GetResponsibleEmployees(objectiveID);
            else if (User.IsInRole("HR"))
                res = _HR.GetResponsibleEmployees(objectiveID);
            else
                res = _founder.GetResponsibleEmployees(objectiveID);

            return Ok(res);
        }

        [HttpGet("/timetest/{objectiveID:int}")]
        [Authorize(Roles = "Responsible")]
        public IActionResult TimeTest(int objectiveID)
        {
            ControllersInit();

            List<EmployeeView> res = _responsible.TimeTest(objectiveID);

            return Ok(res);
        }
    }
}
