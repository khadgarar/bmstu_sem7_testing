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
    public class EmployeesController : BaseController
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

        public EmployeesController(transfersystemContext dbContext)
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
        public IActionResult Get()
        {
            ControllersInit();

            List<EmployeeView> res;
            if (User.IsInRole("Employee"))
                res = _employee.GetAllEmployees();
            else if (User.IsInRole("Responsible"))
                res = _responsible.GetAllEmployees();
            else if (User.IsInRole("Manager"))
                res = _manager.GetAllEmployees();
            else if (User.IsInRole("HR"))
                res = _HR.GetAllEmployees();
            else
                res = _founder.GetAllEmployees();

            return Ok(res);
        }

        [HttpGet("/employees/{employeeID:int}")]
        [Authorize(Roles = "Manager, Founder")]
        public IActionResult GetResponsibilityByEmployee(int employeeID)
        {
            ControllersInit();

            List<Responsibility> res;
            if (User.IsInRole("Manager"))
                res = _manager.GetResponsibilityByEmployee(employeeID);
            else
                res = _founder.GetResponsibilityByEmployee(employeeID);

            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "HR, Founder")]
        public IActionResult Post(EmployeeUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("HR"))
                res = _HR.AddEmployee(value.User_, value.Permission_, value.Department);
            else
                res = _founder.AddEmployee(value.User_, value.Permission_, value.Department);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("/employees/{employeeID:int}")]
        [Authorize(Roles = "HR, Founder")]
        public IActionResult Delete(int employeeID)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("HR"))
                res = _HR.DeleteEmployee(employeeID);
            else
                res = _founder.DeleteEmployee(employeeID);

            if (!res)
                return BadRequest();

            return Ok();
        }
    }
}
