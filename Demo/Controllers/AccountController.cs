using Demo.DataModels;
using Demo.Models.Account;
using Demo.Repositories.Auth;
using Demo.Repositories.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _auth;
        private readonly IJwtHelperRepository _jwtHelper;
        public AccountController(IAccountRepository auth, IJwtHelperRepository jwtHelper)
        {
            _auth = auth;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        [Route("signup")]
        public async Task<object> SignUp([FromBody] User user)
        {
            return await _auth.SignUpUser(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<object> Login([FromBody] User user)
        {
            return await _auth.CheckLoginCredentials(user);
        }
    }
}
