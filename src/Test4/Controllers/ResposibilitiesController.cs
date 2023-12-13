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
    public class ResposibilitiesController : BaseController
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

        public ResposibilitiesController(transfersystemContext dbContext)
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

        [HttpPost]
        [Authorize(Roles = "Manager, Responsible, Founder")]
        public IActionResult Post(ResponsibilityUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Responsible"))
                res = _responsible.AddResponsibility(value.Employee, value.Objective, value.Timeamount);
            else if (User.IsInRole("Manager"))
                res = _manager.AddResponsible(value.Employee, value.Objective);
            else
                res = _founder.AddResponsible(value.Employee, value.Objective);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Responsible")]
        public IActionResult Put(ResponsibilityUI value)
        {
            ControllersInit();

            bool res = _responsible.AddResponsibility(value.Employee, value.Objective, value.Timeamount);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("/resposibilities/{employeeID:int}/{objectiveID:int}")]
        [Authorize(Roles = "Manager, Responsible, Founder")]
        public IActionResult Delete(int employeeID, int objectiveID)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Responsible"))
                res = _responsible.DeleteResponsibility(employeeID, objectiveID);
            else if (User.IsInRole("Manager"))
                res = _manager.DeleteResponsibility(employeeID, objectiveID);
            else
                res = _founder.DeleteResponsibility(employeeID, objectiveID);

            if (!res)
                return BadRequest();

            return Ok();
        }
    }
}
