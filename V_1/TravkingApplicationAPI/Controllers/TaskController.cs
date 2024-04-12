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
        public async Task<ActionResult> AddTask([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var temp = data.GetProperty("task").GetRawText();
                var task = JsonSerializer.Deserialize<AddTask>(temp);
                var res = await taskService.AddnewTask(task);
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
        //[Authorize(Roles ="Mentor")]
        [DisableRequestSizeLimit]
        [Route("GetTask")]
        public async Task<ActionResult> GetTask([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var BatchId = data.GetProperty("BatchId").GetInt32();
                //var BatchId = JsonSerializer.Deserialize<UserTask>(temp);
                var res = await taskService.GetAllTasks(BatchId);
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
        //[Authorize(Roles ="Mentor")]
        [DisableRequestSizeLimit]
        [Route("AddnewSubtask")]
        public async Task<ActionResult> AddnewSubtask([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {

                var temp = data.GetProperty("Subtask").GetRawText();
                var Subtask = JsonSerializer.Deserialize<AddSubTask>(temp);
                var res = await taskService.AddNewSubtask(Subtask);
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
        //[Authorize(Roles ="Mentor")]
        [DisableRequestSizeLimit]
        [Route("GetSubtaskByTask")]
        public async Task<ActionResult> GetSubtaskByTask([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {

                var TaskId = data.GetProperty("TaskId").GetInt32();
                // var Subtask = JsonSerializer.Deserialize<AddSubTask>(temp);
                var res = await taskService.GetAllSubtask(TaskId);
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
        //[Authorize(Roles ="Mentor")]
        [DisableRequestSizeLimit]
        [Route("GetTasksForUser")]
        public async Task<ActionResult> GetTasksForUser([FromBody] int userid)//Try [FromBody]
        {
            try
            {

                //var TaskId = data.GetProperty("TaskId").GetInt32();
                // var Subtask = JsonSerializer.Deserialize<AddSubTask>(temp);
                var res = await taskService.GetTaskforUser(userid);
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