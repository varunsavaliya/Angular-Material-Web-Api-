using Demo.DataModels;

namespace Demo.Repositories.Auth
{
    public interface IAuth
    {
        public User SignUpUser(User user);
        public User CheckLoginCredentials(User user);
    }
}
