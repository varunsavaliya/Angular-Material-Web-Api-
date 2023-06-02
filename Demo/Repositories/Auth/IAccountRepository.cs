using Demo.DataModels;
using System.Threading.Tasks;

namespace Demo.Repositories.Auth
{
    public interface IAccountRepository
    {
        public Task<object> SignUpUser(User user);
        public Task<object> CheckLoginCredentials(User user);
    }
}
