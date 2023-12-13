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
    public class DepartmentsController : BaseController
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

        public DepartmentsController(transfersystemContext dbContext)
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

            List<Department> res;
            if (User.IsInRole("Employee"))
                res = _employee.GetAllDepartments();
            else if (User.IsInRole("Responsible"))
                res = _responsible.GetAllDepartments();
            else if (User.IsInRole("Manager"))
                res = _manager.GetAllDepartments();
            else if (User.IsInRole("HR"))
                res = _HR.GetAllDepartments();
            else
                res = _founder.GetAllDepartments();

            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Manager, Founder")]
        public IActionResult Post(DepartmentUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.AddDepartment(value.Title, value.Foundationyear, value.Activityfield);
            else
                res = _founder.AddDepartment(value.Title, value.Foundationyear, value.Activityfield);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpPut("/departments/{departmentID:int}")]
        [Authorize(Roles = "Manager, Founder")]
        public IActionResult Put(int departmentID, DepartmentUI value)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.UpdateDepartment(departmentID, value.Title, value.Foundationyear, value.Activityfield);
            else
                res = _founder.UpdateDepartment(departmentID, value.Title, value.Foundationyear, value.Activityfield);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("/departments/{departmentID:int}")]
        [Authorize(Roles = "Manager, Founder")]
        public async Task<IActionResult> Delete(int departmentID)
        {
            ControllersInit();

            bool res;
            if (User.IsInRole("Manager"))
                res = _manager.DeleteDepartment(departmentID);
            else
                res = _founder.DeleteDepartment(departmentID);

            if (!res)
                return BadRequest();

            if (_manager.CheckEmployeeDepartment(departmentID))
            {
                var identity = User.Identity as ClaimsIdentity;
                identity.RemoveClaim(User.Claims.Where(el => el.Type == ClaimTypes.Role).Single());

                var EmployeeClaim = User.Claims.Where(el => el.Type == "EmployeeID").SingleOrDefault();
                if (EmployeeClaim != null)
                    identity.RemoveClaim(EmployeeClaim);

                identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }

            return Ok();
        }
    }
}
