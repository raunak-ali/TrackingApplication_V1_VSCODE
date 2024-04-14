using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;
using TravkingApplicationAPI.Services;

namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly UserService UserService;

        public UserController(UserService UserProfileServices)
        {
            this.UserService = UserProfileServices;
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        [Route("AddMentor")]
      
        public async Task<ActionResult> AddUser([FromBody] AddUserAddUser user)//Try [FromBody]
        {
            try
            {
                var res = await UserService.AddUser(user);
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }








        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]

        public async Task<ActionResult> LoginUserProfile([FromBody] Login user)//Try [FromBody]
        {
            try
            {

                var res = await UserService.LoginUser(user);
                if (res == null)
                {
                    return BadRequest(new {message="Username or password is incorrect"});
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Mentor,Employee,Admin")]
        [Route("GetUserByBatch")]

        public async Task<ActionResult> GetUserByBatch([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var BatchId = data.GetProperty("BatchId").GetInt32();
                var res = await UserService.GetUserByBatch(BatchId);
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetUserinfo")]

        public async Task<ActionResult> GetUserinfo([FromBody] int userid)//Try [FromBody]
        {
            try
            {

                var res = await UserService.GetUser(userid);
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("FetchMetors")]

        public async Task<ActionResult> FetchMentors()//Try [FromBody]
        {
            try
            {

                var res = await UserService.FetchMentors();
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


         [HttpPost]
        [AllowAnonymous]
        [Route("ResetPasswordOtp")]

        public async Task<ActionResult> ResetPasswordOtp([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
string capgeminiid= data.GetProperty("capgeminiid").ToString();

                var res = await UserService.ResetPasswordOtp(capgeminiid);
                if (res == null)
                {
                    return BadRequest(new {message="User with that email does not exist"});
                }
                return Ok(new {message=res});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
[HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]

        public async Task<ActionResult> ResetPassword([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var username= data.GetProperty("username").ToString();
                var oldpassword=data.GetProperty("oldpassword").ToString();
                var newpassword=data.GetProperty("newpassword").ToString();

                var res = await UserService.ResetPassword(username,oldpassword,newpassword);
                if (res == null)
                {
                    return BadRequest(new {message="User does not exist,Recheck the username and old password"});
                }
                return Ok(new {message=res});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


           [HttpGet]
        [Authorize(Roles = "Mentor,Employee,Admin")]
        [Route("GetEmployees")]

        public async Task<ActionResult> GetEmployees()//Try [FromBody]
        {
            try
            {
                
                var res = await UserService.FetchEmployees();
                if (res == null)
                {
                    return BadRequest();
                }
                return Ok(new { message = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}