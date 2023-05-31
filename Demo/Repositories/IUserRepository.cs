using Demo.DataModels;
using Demo.Models.Common;
using Demo.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public interface IUserRepository
    {
        public object GetAllUsers();
        public object GetUserById(long userId);
        public Task<object> CreateUser(User user);
        public object UpdateUser(User user, long userId);

        public object DeleteUser(long userId);
    }
}
