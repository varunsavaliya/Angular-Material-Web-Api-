using Demo.Models.Common;
using Demo.Models.User;

namespace Demo.Models.Account
{
    public class AccountModel : ResponseDataModel<UserModel>
    {
        public string Token { get; set; }
    }
}
