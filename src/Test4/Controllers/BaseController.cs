using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test4.Controllers
{
    public class BaseController : ControllerBase
    {
        private string _username;

        protected string Username
        {
            get
            {
                if (_username == null)
                {
                    _username = User.Identity.Name;
                }
                return _username;
            }
        }
    }
}
