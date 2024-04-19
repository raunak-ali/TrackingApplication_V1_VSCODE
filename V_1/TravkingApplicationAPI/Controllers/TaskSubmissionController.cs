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
using TravkingApplicationAPI.Services;

namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class TaskSubmissionController : Controller
    {
        private readonly TaskSubmissionService taskSubmissionService;

        public TaskSubmissionController(TaskSubmissionService taskSubmissionService)
        {
            this.taskSubmissionService = taskSubmissionService;
        }

        [HttpPost]
        //[Authorize(Roles ="Admin")]
        [Route(" GetSubmOfaSubtaskbyUser")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSubmOfaSubtaskbyUser([FromBody] dynamic data)
        {
            //Used to fetch for a Mentor/Admin

            try
            {
                // Access subtaskid and userid properties from the JsonElement
                int subtaskid = data.GetProperty("subtaskid").GetInt32();
                int userid = data.GetProperty("userid").GetInt32();

                var res = taskSubmissionService.GetSubmOfaSubtaskbyUser(subtaskid, userid);

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
        //[Authorize(Roles ="Admin")]
        [Route("GetSubmOfaSubtask")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSubmOfaSubtask([FromBody] dynamic data)
        {
            //Geting all responses for one subtask
            try
            {
                int subtaskid = data.GetProperty("subtaskid").GetInt32();
                var res = taskSubmissionService.GetSubmOfaSubtask(subtaskid);
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
        //[Authorize(Roles ="Admin")]
        [Route("AddSubmission")]
        [AllowAnonymous]
        public async Task<ActionResult> AddSubmission([FromBody] dynamic data)
        {
            //Update the existing subtask with the new EMployee  input

            try
            {
                var temp = data.GetProperty("taskSubmissions").GetRawText();
                var taskSubmissions = JsonSerializer.Deserialize<AddTaskSubmission>(temp);

                var res = taskSubmissionService.AddSubmission(taskSubmissions);
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
        //[Authorize(Roles ="Admin")]
        [Route("RateASubmittedTask")]
        [AllowAnonymous]
        public async Task<ActionResult> RateASubmittedTask([FromBody] dynamic data)
        {


            try
            {
                var temp = data.GetProperty("addRating").GetRawText();
                var addRating = JsonSerializer.Deserialize<AddRating>(temp);
                var res = taskSubmissionService.RateASubmittedTask(addRating);
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
        [Route("UpdateFeedbacks")]
        public async Task<ActionResult> UpdateFeedbacks([FromBody] dynamic data)
        {
       
            try
            {
                var temp = data.GetProperty("Feedbacks").GetProperty("feedbacks").ToString();
                //temp = data.GetProperty("feedbacks").ToString();
        var feedbacks = System.Text.Json.JsonSerializer.Deserialize<List<AddFeedback>>(temp);
                var res = taskSubmissionService.UpdateFeedbacks(feedbacks);
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