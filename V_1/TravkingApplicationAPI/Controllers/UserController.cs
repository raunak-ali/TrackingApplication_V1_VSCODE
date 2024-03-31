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

public async Task<ActionResult>  AddUser([FromBody]AddUser user)//Try [FromBody]
{
try{ 
    var res = await UserService.AddUser(user);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(new{message=res});
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}








        [HttpPost]
[AllowAnonymous]
[Route("Login")]

public async Task<ActionResult>  LoginUserProfile([FromBody]Login user)//Try [FromBody]
{
    try
    {
        
        var res = await UserService.LoginUser(user);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

    }
}