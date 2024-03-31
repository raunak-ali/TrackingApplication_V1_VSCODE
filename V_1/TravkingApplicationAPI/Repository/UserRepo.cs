using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;
using Newtonsoft.Json;

namespace TravkingApplicationAPI.Repository
{
    public class UserRepo:IUser
    {
         TrackingApplicationDbContext  context;
         static IConfiguration ? _config;

       
        public UserRepo(DbContextOptions<TrackingApplicationDbContext> options,IConfiguration configuration){
            context=  new TrackingApplicationDbContext(options);
            _config=configuration;
            }
 public static string GenerateToken(User user)
    {
        
       
            var claims = new List<Claim>{
            
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
             var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        _config["Jwt:Issuer"],
        _config["Jwt:Audience"],
        claims,
        expires: DateTime.Now.AddMinutes(1),
        signingCredentials: credentials
    );
return new JwtSecurityTokenHandler().WriteToken(token);}
        public async Task<string> LoginUser(Login user)
        {
            try{
                if(user!=null){
                    var existing_user=context.Users.FirstOrDefault(s=>s.UserName==user.UserName && s.Password==user.Password);
if(existing_user!=null){
string Token=GenerateToken(existing_user);
                    //Adding that to Json response
                    var responseJson = new
                {
                    token = Token,
                    userProfile = existing_user
                };
                  string jsonResponse = JsonConvert.SerializeObject(responseJson);
                return jsonResponse;
}
                }
                return null;
            }
            catch(Exception e){
                throw;
            }
        }

        public async Task<string> AddUser(AddUser user)
        {
            try{
                if(user!=null){
                    //Generate a username
                    string username=user.Name.Substring(0,3)+"_"+user.Domain.Substring(3,user.Domain.Length);
                    string password = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(10)).Substring(0, 12);
//Adding the generateed Username and Password to the existing user
User newuser=new User();
newuser.UserName=username;
newuser.Password=password;
newuser.Name=user.Name;
newuser.Role=user.Role;
newuser.Domain=user.Domain;
newuser.JobTitle=user.JobTitle;
newuser.Location=user.Location;
newuser.Phone=user.Phone;
newuser.IsCr=user.IsCr;
newuser.Batches=user.Batches;
newuser.Gender=user.Gender;
newuser.Doj=user.Doj;
newuser.CapgeminiEmailId=user.CapgeminiEmailId;
newuser.Grade=user.Gender;
newuser.Total_Average_RatingStatus=0;//Default value starts at 0
newuser.PersonalEmailId=user.PersonalEmailId;
newuser.EarlierMentorName=user.EarlierMentorName;
newuser.FinalMentorName=user.FinalMentorName;
newuser.Attendance_Count=0;//Initailzed to zero by defult
newuser.DailyUpdates=null;

//Now lets add this to the our db
context.Users.Add(newuser);
await context.SaveChangesAsync();
return "User sucessfully added";
                }
                return null;
            }
            catch(Exception e){
                throw;
            }
        }
    }
}