using JWTToken.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        [Authorize]
        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "Authenticated with Jwt";
        }

        [HttpGet]
        [Route("Details")]
        public string Details()
        {
            return "Authenticated with Jwt";
        }

        [Authorize]
        [HttpPost]
        [Route("AddUser")]
        public string AddUser(Users user)
        {
            return "User added with username"+user.Username;
        }
    }
}
