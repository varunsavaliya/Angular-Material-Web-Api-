using System;
using System.Collections;
using System.Collections.Generic;

namespace Demo.DataModels
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public ICollection<SMSTemplate> CreatedBySMSTemplate { get; set; }
        public ICollection<SMSTemplate> UpdatedBySMSTemplate { get; set; }
    }
}
