using Demo.DataModels;

namespace Demo.Repositories.JWT
{
    public interface IJwtHelper
    {
        public string GetJwtToken(User user);
    }
}
