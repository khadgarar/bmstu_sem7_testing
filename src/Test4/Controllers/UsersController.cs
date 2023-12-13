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
    public class UsersController : BaseController
    {
        private readonly transfersystemContext db;

        private IUserRepository userRep;
        private ICompanyRepository companyRep;
        private IDepartmentRepository departmentRep;
        private IEmployeeRepository employeeRep;

        User user;
        UserController _user;

        public UsersController(transfersystemContext dbContext)
        {
            db = dbContext;

            userRep = new UserRepository(db);
            companyRep = new CompanyRepository(db);
            departmentRep = new DepartmentRepository(db);
            employeeRep = new EmployeeRepository(db);
        }

        private void ControllersInit()
        {
            user = userRep.GetUserByLogin(Username);
            _user = new(user, userRep, companyRep, departmentRep, employeeRep);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUser()
        {
            ControllersInit();

            UserView res = _user.GetUser();

            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put(UserUI value)
        {
            ControllersInit();

            bool res = _user.UpdateUser(value.Password_, value.Name_, value.Surname);

            if (!res)
                return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            ControllersInit();

            bool res = _user.DeleteUser();

            if (!res)
                return BadRequest();

            return Ok();
        }
    }
}
