using Demo.DataModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models.User;
using Demo.Models.Common;
using System;
using AutoMapper;
using Demo.Models;

namespace Demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<object> GetAllUsers()
        {
            ResponseListModel<UserModel> userList = new();
            userList.Items = new List<UserModel>();
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                foreach (var user in users)
                {
                    UserModel userModel = new();
                    userModel = _mapper.Map<UserModel>(user);
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

        public async Task<object> GetUserById(long userId)
        {
            ResponseDataModel<UserModel> responseDataModel = new();
            UserModel userModel = new();
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                userModel = _mapper.Map<UserModel>(user);

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
            ResponseModel responseModel = new();
            UserModel userModel = new();
            try
            {
                User isUserExists = _dbContext.Users.FirstOrDefault(u => u.Username.Equals(user.Username));
                if (isUserExists != null)
                {
                    responseModel.Message = "user already exists!";
                    responseModel.Success = false;
                }
                else
                {
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    User createdUser = await _dbContext.Users.FirstOrDefaultAsync(user1 => user1.Username == user.Username);

                    responseModel.Message = createdUser.Username + " has been created successfully";
                    responseModel.Success = true;
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }
        public async Task<object> UpdateUser(User updatedUser, long userId)
        {
            ResponseModel responseModel = new();
            try
            {
                var isUserExists = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username.Equals(updatedUser.Username) && user.UserId != updatedUser.UserId);
                if (isUserExists != null)
                {
                    responseModel.Message = "User already Exists";
                    responseModel.Success = false;
                }
                else
                {
                    var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                    if (user != null)
                    {
                        user.Username = updatedUser.Username;
                        user.Password = updatedUser.Password;
                        user.Address = updatedUser.Address;
                        user.DOB = updatedUser.DOB;

                        _dbContext.SaveChanges();

                        responseModel.Message = user.Username + " has been updated successfully!";
                        responseModel.Success = true;
                    }
                    else
                    {
                        responseModel.Message = "User not found";
                        responseModel.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }
            return responseModel;
        }
        public async Task<object> DeleteUser(long userId)
        {
            ResponseModel responseModel = new();
            UserModel userModel = new();
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();

                    responseModel.Message = user.Username + " has been deleted successfully";
                    responseModel.Success = true;
                }
                else
                {
                    responseModel.Message = "User not found";
                    responseModel.Success = false;
                }
            }
            catch (Exception ex)
            {
                responseModel.Message = ex.Message;
                responseModel.Success = false;
            }

            return responseModel;
        }

    }
}
