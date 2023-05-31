using Demo.DataModels;
using Demo.Repositories.Auth;
using Demo.Repositories.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAuth _auth;
        private readonly IJwtHelper _jwtHelper;

        public AuthController(IAuth auth, IConfiguration configuration, IJwtHelper jwtHelper)
        {
            _auth = auth;
            _jwtHelper = jwtHelper;
        }

        [HttpPost]
        [Route("signup")]
        public object SignUp([FromBody] User user)
        {
            User userResponseData = _auth.SignUpUser(user);
            if (userResponseData != null)
            {
                string jwtToken = _jwtHelper.GetJwtToken(user);
                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Ok(new { respone = false });
                }
                return Ok(new { response = true, token = jwtToken, userResponseData = userResponseData });
            }

            return Ok(new { respone = false });
        }

        [HttpPost]
        [Route("login")]
        public object Login([FromBody] User user)
        {
            User userResponseData = _auth.CheckLoginCredentials(user);
            if (userResponseData != null)
            {
                string jwtToken = _jwtHelper.GetJwtToken(user);
                if (string.IsNullOrEmpty(jwtToken))
                {
                    return Ok(new { respone = false });
                }
                return Ok(new { response = true, token = jwtToken, userResponseData = userResponseData });
            }

            return Ok(new { respone = false });
        }
    }
}
