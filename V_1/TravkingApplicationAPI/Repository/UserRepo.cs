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
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

namespace TravkingApplicationAPI.Repository
{
    public class UserRepo : IUser
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;


        public UserRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
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
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> LoginUser(Login user)
        {
            try
            {
                if (user != null)
                {
                    var existing_user = context.Users.FirstOrDefault(s => s.UserName == user.UserName && s.Password == user.Password);
                    if (existing_user != null)
                    {
                        string Token = GenerateToken(existing_user);
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
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<User> AddUser(AddUserAddUser user)
        {
            try
            {
                if (user != null)
                {
                    //Check if the User with that username already exixts,If it does just return that it already exists
                    var existing_user=context.Users.FirstOrDefault(u=>u.Phone==user.Phone && u.CapgeminiEmailId==user.CapgeminiEmailId);
                    if(existing_user==null){
                    //Generate a username
                    string username = user.Name.Substring(0, 3) + "_" + user.Domain.Substring(0, Math.Min(3, user.Domain.Length))+"_"+Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(2)).Substring(0,1);
                    string password = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(10)).Substring(0, 12);
                    //Adding the generateed Username and Password to the existing user

                    //
                    User newuser = new User();
                    newuser.UserName = username;
                    newuser.Password = password;
                    newuser.Name = user.Name;
                    newuser.Role = user.Role;
                    newuser.Domain = user.Domain;
                    newuser.JobTitle = user.JobTitle;
                    newuser.Location = user.Location;
                    newuser.Phone = user.Phone;
                    newuser.IsCr = user.IsCr;//Dont take it from excel
                    newuser.Batches = user.Batches;
                    newuser.Gender = user.Gender;
                    newuser.Doj = user.Doj;
                    newuser.CapgeminiEmailId = user.CapgeminiEmailId;
                    newuser.Total_Average_RatingStatus = 0;//Default value starts at 0
                    newuser.PersonalEmailId = user.PersonalEmailId;
                    newuser.EarlierMentorName = user.EarlierMentorName;
                    newuser.FinalMentorName = user.FinalMentorName;
                    newuser.Attendance_Count = 0;//Initailzed to zero by defult
                    newuser.DailyUpdates = null;//No no

                    //Now lets add this to the our db
                    context.Users.Add(newuser);
                    await context.SaveChangesAsync();
                    return newuser;}
                    else{
                        
                        return existing_user;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<User>> GetUserByBatch(int BatchId)
        {
            try
            {
                if (BatchId != null)
                {
                    var existing_batch = context.Batches.FirstOrDefault(b => b.BatchId == BatchId);
                    if (existing_batch != null)
                    {
                        var Batch_Employees = context.Users.Where(u => u.Batches.Any(b => b == existing_batch) && u.Role == Role.Employee).ToList();
                        if (Batch_Employees != null && Batch_Employees.Count > 0)
                        {
                            return Batch_Employees;


                        }
                        //NO Employyes in this batch
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<User> GetUser(int Userid)
        {
            //get user info
          try{
            if(Userid!=null){
                var exsting_user=context.Users.FirstOrDefault(u=>u.UserId==Userid);
                if(exsting_user!=null){
return exsting_user;}
return null;
            }
            return null;
          }
          catch(Exception e){throw;}
        }

        public async Task<List<User>> FetchMentors()
        {
            var existing_mentors=context.Users.Where(u=>u.Role==Role.Mentor).ToList();
            if(existing_mentors !=null){
                return existing_mentors;
            }
            return null;
          
        }

        public Task<List<User>> FetchAllEmployeesUnderAMentor(int Userid)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ResetPasswordOtp(string capgeminiid)
        {
            try{
var existing_user=context.Users.FirstOrDefault(u=>u.CapgeminiEmailId==capgeminiid);
if(existing_user!=null){
    var reset_password=Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(10)).Substring(0, 12);
    existing_user.Password=reset_password;
    context.SaveChanges();
    //SendEmail(capgeminiid,existing_user.UserName,"Password reset",reset_password); ->FOr now commented out as i dont wanna send actual emails on capgemini id's
    return "OneTime Password has been sent to your email";
}
                return "User with this email id is not registered";
            }
            catch(Exception e){throw;}
            return null;

           //First check if a user with that capgemini email id is registered, if so then send an otp to thier email address
           //Generate a onetimepassword with which they can use the reset password option
           //

        }

         public void SendEmail(string toEmail,string username, string subject,string password)
        {
            try{
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            
            client.Credentials = new NetworkCredential("netasp709@gmail.com", "ndeq qwol oyew bxxr");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("netasp709@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Reset Password</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>You can Reset your password, By entering this in the past password </p>");
            mailBody.AppendFormat("<p>Username : {0}</p>", username);
            mailBody.AppendFormat("<p>Password : {0}</p>", password); // Include Account Number
    mailMessage.Body = mailBody.ToString();

    // Send email
    client.Send(mailMessage);}
    catch(Exception ex){
        //Lets not throw an exception here since this will not work on office VPN
        return;
    }
        }

        public async Task<string> ResetPassword(string username, string oldpassword, string newpassword)
        {
           try{
            var existing_user=context.Users.FirstOrDefault(u=>u.UserName==username && u.Password==oldpassword);
            if(existing_user!=null){
                existing_user.Password=newpassword;
                context.SaveChanges();
            }

            return "User with that username and password does not exist";
           }
           catch(Exception e){
            throw;}
        }
    }
}