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
        public async Task<object> GetUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> GetUserById(long id)
        {
            return await _userRepository.GetUserById(id); ;
        }

        [HttpPost]
        public async Task<object> CreateUser([FromBody] User user)
        {
            return await _userRepository.CreateUser(user);
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateUser(long id, User updatedUser)
        {
            return await _userRepository.UpdateUser(updatedUser, id);
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteUser(long id)
        {
            return await _userRepository.DeleteUser(id);
        }

    }
}
