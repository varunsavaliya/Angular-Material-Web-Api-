using System;

namespace Demo.Models.User
{
    public class UserModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
