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
    public class CompaniesController : BaseController
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
        FounderController _founder;

        public CompaniesController(transfersystemContext dbContext)
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

            if (User.IsInRole("Founder"))
            {
                var identity = User.Identity as ClaimsIdentity;
                int eid = int.Parse(identity.FindFirst("EmployeeID").Value);
                employee = employeeRep.GetEmployeeByID(eid);
                _founder = new(user, employee, userRep, companyRep, departmentRep, employeeRep, objectiveRep, responsibilityRep);
            }
            _user = new(user, userRep, companyRep, departmentRep, employeeRep);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(CompanyUI value)
        {
            ControllersInit();

            bool res = _user.AddCompany(value.Title, value.Foundationyear);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Founder")]
        public IActionResult Put(CompanyUI value)
        {
            ControllersInit();

            bool res = _founder.UpdateCompany(value.Title, value.Foundationyear);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Founder")]
        public async Task<IActionResult> Delete()
        {
            ControllersInit();

            bool res = _founder.DeleteCompany();

            if (!res)
                return BadRequest();

            var identity = User.Identity as ClaimsIdentity;
            identity.RemoveClaim(User.Claims.Where(el => el.Type == ClaimTypes.Role).Single());

            var EmployeeClaim = User.Claims.Where(el => el.Type == "EmployeeID").SingleOrDefault();
            if (EmployeeClaim != null)
                identity.RemoveClaim(EmployeeClaim);

            identity.AddClaim(new Claim(ClaimTypes.Role, "User"));

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return Ok();
        }
    }
}
