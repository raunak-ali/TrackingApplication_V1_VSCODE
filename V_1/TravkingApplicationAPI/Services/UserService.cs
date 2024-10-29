using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class UserService
    {
        public IUser User;
        public UserService(IUser User)
        {
            this.User = User;
        }

        public async Task<string> LoginUser(Login user)
        {

            try
            {

                return await User.LoginUser(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> AddUser(AddUserAddUser user)
        {
            try
            {
                return await User.AddUser(user);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<User>> GetUserByBatch(int BatchId)
        {
            try
            {
                return await User.GetUserByBatch(BatchId);


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUser(int Userid)
        {
            try
            {
                return await User.GetUser(Userid);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<User>> FetchMentors()
        {
            try
            {
                return await User.FetchMentors();


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> ResetPasswordOtp(string capgeminiid)
        {
            try
            {
                return await User.ResetPasswordOtp(capgeminiid);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> ResetPassword(string username, string oldpassword, string newpassword)
        {
            try
            {
                return await User.ResetPassword(username, oldpassword, newpassword);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<User>> FetchEmployees()
        {

            try
            {
                return await User.FetchEmployees();


            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}