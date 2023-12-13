using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
    public class WorkplacesController : BaseController
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
        UserController _user;
        EmployeeController _employee;

        public WorkplacesController(transfersystemContext dbContext)
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

            if (!User.IsInRole("User"))
            {
                var identity = User.Identity as ClaimsIdentity;
                int eid = int.Parse(identity.FindFirst("EmployeeID").Value);
                employee = employeeRep.GetEmployeeByID(eid);
                _employee = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            }
            _user = new(user, userRep, companyRep, departmentRep, employeeRep);
        }

        [Authorize]
        [HttpPost("/workplaces")]
        public async Task<IActionResult> ChooseWorkplace(EmployeeUI val)
        {
            User user = userRep.GetUserByLogin(Username);
            UserController _user = new(user, userRep, companyRep, departmentRep, employeeRep);

            Employee employee = _user.GetEmployeeByWorkplace(val.EmployeeID);
            if (employee == null)
                return BadRequest();

            var identity = User.Identity as ClaimsIdentity;
            identity.RemoveClaim(User.Claims.Where(el => el.Type == ClaimTypes.Role).Single());

            var EmployeeClaim = User.Claims.Where(el => el.Type == "EmployeeID").SingleOrDefault();
            if (EmployeeClaim != null)
                identity.RemoveClaim(EmployeeClaim);

            if (employee.Permission_ == 0)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Employee"));
            else if (employee.Permission_ == 1)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Responsible"));
            else if (employee.Permission_ == 2)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Manager"));
            else if (employee.Permission_ == 3)
                identity.AddClaim(new Claim(ClaimTypes.Role, "HR"));
            else if (employee.Permission_ == 4)
                identity.AddClaim(new Claim(ClaimTypes.Role, "Founder"));
            else
                return BadRequest();

            identity.AddClaim(new Claim("EmployeeID", employee.Employeeid.ToString()));

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetWorkplaces()
        {
            ControllersInit();

            List<WorkplaceView> res = _user.GetWorkplaces();

            return Ok(res);
        }

        [HttpGet("/workplaces/current")]
        [Authorize(Roles = "Employee, Manager, Responsible, HR, Founder")]
        public IActionResult GetCurrentWorkplace()
        {
            ControllersInit();

            WorkplaceView res = _employee.GetWorkplace();

            return Ok(res);
        }
    }
}
