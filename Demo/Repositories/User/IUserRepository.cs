using Demo.DataModels;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public interface IUserRepository
    {
        public Task<object> GetAllUsers();
        public Task<object> GetUserById(long userId);
        public Task<object> CreateUser(User user);
        public Task<object> UpdateUser(User user, long userId);
        public Task<object> DeleteUser(long userId);
    }
}
