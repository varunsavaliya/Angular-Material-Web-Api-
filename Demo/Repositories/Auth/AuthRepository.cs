using Demo.DataModels;
using System;
using System.Linq;

namespace Demo.Repositories.Auth
{
    public class AuthRepository : IAuth
    {
        private readonly AppDbContext _dbContext;

        public AuthRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User CheckLoginCredentials(User user)
        {
            var users = _dbContext.Users.ToList(); 

            User userResponseData = users.FirstOrDefault(user1 =>
                user1.Username.Equals(user.Username) &&
                user1.Password.Equals(user.Password));

            return userResponseData;
        }


        public User SignUpUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var users = _dbContext.Users.ToList();

            User userResponseData = users.FirstOrDefault(user1 =>
              user1.Username.Equals(user.Username) &&
              user1.Password.Equals(user.Password));

            return userResponseData;
        }
    }
}
