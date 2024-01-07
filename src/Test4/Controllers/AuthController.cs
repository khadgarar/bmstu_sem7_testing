using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Test4.Models;

namespace Test4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IUserRepository userRep;
        private ICompanyRepository companyRep;
        private IDepartmentRepository departmentRep;
        private IEmployeeRepository employeeRep;

        private NotAuthController _notAuth;

        private readonly transfersystemContext db;
        public AuthController(transfersystemContext dbContext)
        {
            db = dbContext;

            userRep = new UserRepository(db);
            companyRep = new CompanyRepository(db);
            departmentRep = new DepartmentRepository(db);
            employeeRep = new EmployeeRepository(db);

            _notAuth = new NotAuthController(userRep);
        }

        [HttpPost("/auths/register")]
        public IActionResult Register(UserUI value)
        {
            bool res = _notAuth.AddUser(value.Login, value.Password_, value.Name_, value.Surname);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthUI val)
        {
            var user = _notAuth.GetUserByLogin(val.Username);

            if (val.Password == user.Password_)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, val.Username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, val.Username));
                claims.Add(new Claim(ClaimTypes.Role, "User"));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);

                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("/auths/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
