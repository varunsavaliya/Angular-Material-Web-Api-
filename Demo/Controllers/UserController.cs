using Demo.DataModels;
using Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public object GetUsers()
        {
            return _userRepository.GetAllUsers();
        }

        [HttpGet]
        [Route("{id}")]
        public object GetUserById(long id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<object> CreateUser([FromBody]User user)
        {
           object createdUser = await _userRepository.CreateUser(user);
            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public object UpdateUser(long id, User updatedUser)
        {
           object updateUser =  _userRepository.UpdateUser(updatedUser, id);
            return Ok(updateUser);
        }

        [HttpDelete("{id}")]
        public object DeleteUser(long id)
        {
            object deletedUser = _userRepository.DeleteUser(id);
            return Ok(deletedUser);
        }

    }
}
