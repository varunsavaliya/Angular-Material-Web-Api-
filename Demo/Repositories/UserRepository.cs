using Demo.DataModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models.User;
using Demo.Models.Common;
using System;

namespace Demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public object GetAllUsers()
        {
            ResponseListModel<UserModel> userList = new();
            userList.Items = new List<UserModel>();
            try
            {
                var users = _dbContext.Users.ToList();
                foreach (var user in users)
                {
                    UserModel userModel = new();
                    userModel.UserId = user.UserId;
                    userModel.Username = user.Username;
                    userModel.Password = user.Password;
                    userModel.Address = user.Address;
                    userModel.DOB = user.DOB;

                    userList.Items.Add(userModel);
                }

                userList.Success = true;
                userList.Message = "success";
            }
            catch (Exception ex)
            {
                userList.Message = ex.Message;
                userList.Success = false;
            }
            return userList;
        }

        public object GetUserById(long userId)
        {
            ResponseDataModel<UserModel> responseDataModel = new();
            UserModel userModel = new();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                userModel.UserId = user.UserId;
                userModel.Username = user.Username;
                userModel.Password = user.Password;
                userModel.Address = user.Address;
                userModel.DOB = user.DOB;

                responseDataModel.Data = userModel;
                responseDataModel.Message = "success";
                responseDataModel.Success = true;
            }
            catch (Exception ex)
            {
                responseDataModel.Data = userModel;
                responseDataModel.Message = ex.Message;
                responseDataModel.Success = false;
            }
            return responseDataModel;
        }

        public async Task<object> CreateUser(User user)
        {
            ResponseDataModel<UserModel> responseDataModel = new();
            UserModel userModel = new();
            try
            {
                user.Password = null;
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                User createdUser = await _dbContext.Users.FirstOrDefaultAsync(user1 => user1.Username == user.Username);
                userModel.UserId = createdUser.UserId;
                userModel.Username = createdUser.Username;
                userModel.Password = createdUser.Password;
                userModel.Address = createdUser.Address;
                userModel.DOB = createdUser.DOB;

                responseDataModel.Data = userModel;
                responseDataModel.Message = "success";
                responseDataModel.Success = true;
            }
            catch (Exception ex)
            {
                responseDataModel.Data = userModel;
                responseDataModel.Message = ex.Message;
                responseDataModel.Success = false;
            }
            return responseDataModel;
        }
        public object UpdateUser(User updatedUser, long userId)
        {
            ResponseDataModel<UserModel> responseDataModel = new();
            UserModel userModel = new();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                user.Username = updatedUser.Username;
                user.DOB = updatedUser.DOB;
                user.Address = updatedUser.Address;

                _dbContext.SaveChanges();

                userModel.UserId = user.UserId;
                userModel.Username = user.Username;
                userModel.Password = user.Password;
                userModel.Address = user.Address;
                userModel.DOB = user.DOB;

                responseDataModel.Data = userModel;
                responseDataModel.Message = "success";
                responseDataModel.Success = true;
            }
            catch (Exception ex)
            {
                responseDataModel.Data = userModel;
                responseDataModel.Message = ex.Message;
                responseDataModel.Success = false;
            }
            return responseDataModel;
        }
        public object DeleteUser(long userId)
        {
            ResponseDataModel<UserModel> responseDataModel = new();
            UserModel userModel = new();
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                userModel.UserId = user.UserId;
                userModel.Username = user.Username;
                userModel.Password = user.Password;
                userModel.Address = user.Address;
                userModel.DOB = user.DOB;

                responseDataModel.Data = userModel;
                responseDataModel.Message = "success";
                responseDataModel.Success = true;
            }
            catch (Exception ex)
            {
                responseDataModel.Data = userModel;
                responseDataModel.Message = ex.Message;
                responseDataModel.Success = false;
            }

            return responseDataModel;
        }

    }
}
