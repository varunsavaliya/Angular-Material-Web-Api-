using AutoMapper;
using Demo.DataModels;
using Demo.Models.Account;
using Demo.Models.Common;
using Demo.Models.User;
using Demo.Repositories.JWT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Repositories.Auth
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IJwtHelperRepository _jwtHelper;

        public AccountRepository(AppDbContext dbContext, IMapper mapper, IJwtHelperRepository jwtHelper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _jwtHelper = jwtHelper;
        }
        public async Task<object> CheckLoginCredentials(User user)
        {
            AccountModel accountModel = new();
            try
            {
                var users = await _dbContext.Users.ToListAsync();

                User validUser = users.FirstOrDefault(user1 =>
                    user1.Username.Equals(user.Username) &&
                    user1.Password.Equals(user.Password));

                    accountModel.Data = _mapper.Map<UserModel>(validUser);
                    accountModel.Token = validUser != null ? _jwtHelper.GetJwtToken(validUser) : null;
                    accountModel.Message = validUser != null ? "success" : "failed";
                    accountModel.Success = validUser != null ? true : false;

            }
            catch (Exception ex)
            {
                accountModel.Success = false;
                accountModel.Message = ex.Message;
            }
            return accountModel;
        }


        public async Task<object> SignUpUser(User user)
        {
            AccountModel accountModel = new();
            try
            {
                    var users = await _dbContext.Users.ToListAsync();
                var isUserExists = users.FirstOrDefault(u =>
                string.Equals(u.Username, user.Username, StringComparison.Ordinal));

                if (isUserExists != null)
                {
                    accountModel.Message = "User already exists!";
                    accountModel.Success = false;
                }
                else
                {
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                     users = await _dbContext.Users.ToListAsync();


                    User userResponseData = users.FirstOrDefault(user1 =>
                      user1.Username.Equals(user.Username) &&
                      user1.Password.Equals(user.Password));

                    string jwtToken = _jwtHelper.GetJwtToken(userResponseData);

                    if (userResponseData != null)
                    {
                        accountModel.Data = _mapper.Map<UserModel>(userResponseData);
                        accountModel.Message = "success";
                        accountModel.Success = true;
                        accountModel.Token = jwtToken;
                    }
                }
            }
            catch (Exception ex)
            {
                accountModel.Message = ex.Message;
                accountModel.Success = false;
            }
            return accountModel;
        }
    }
}
