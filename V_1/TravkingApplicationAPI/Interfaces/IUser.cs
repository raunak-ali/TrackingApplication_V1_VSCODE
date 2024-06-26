using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Interfaces
{
    public interface IUser
    {
Task<string> LoginUser(Login user);
Task<User> AddUser(AddUserAddUser user);

Task<List<User>> GetUserByBatch(int BatchId);

Task<User> GetUser(int Userid);

Task<List<User>>FetchMentors();

Task<List<User>>FetchEmployees();
Task<List<User>>FetchAllEmployeesUnderAMentor(int Userid);

Task<string>ResetPasswordOtp(string capgeminiid);

Task<string>ResetPassword(string username,string oldpassword,string newpassword);
    }
}