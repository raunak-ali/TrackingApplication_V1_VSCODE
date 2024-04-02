using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;
using TravkingApplicationAPI.Services;

namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class TaskController : Controller
    {
     
        private readonly TaskServices taskService;

        public TaskController(TaskServices taskService)
{
    this.taskService = taskService;
}
[HttpPost]
[AllowAnonymous]
//[Authorize(Roles ="Mentor")]
[DisableRequestSizeLimit]
[Route("AddTask")]
public async Task<ActionResult>  AddTask([FromBody]AddTask task)//Try [FromBody]
{
try{ 
    //var temp=data.GetProperty("batch").GetRawText();
    //var batch = JsonSerializer.Deserialize<Addbatch>(temp);
    var res = await taskService.AddnewTask(task);
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
//[Authorize(Roles ="Mentor")]
[DisableRequestSizeLimit]
[Route("GetTask")]
public async Task<ActionResult>  GetTask([FromBody]dynamic data)//Try [FromBody]
{
try{ 
    var BatchId=data.GetProperty("BatchId").GetInt32();
    //var BatchId = JsonSerializer.Deserialize<UserTask>(temp);
    var res = await taskService.GetAllTasks(BatchId);
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


    }
}