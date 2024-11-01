using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;
using TravkingApplicationAPI.Services;
using Newtonsoft.Json;

namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class BatchController : Controller
    {
        private readonly BatchService BatchService;

        public BatchController(BatchService batchService)
        {
            this.BatchService = batchService;
        }
        [HttpPost]
        //[Authorize(Roles ="Admin")]
        [AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("AddBatch")]
        public async Task<ActionResult> AddBatch([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var temp = data.GetProperty("batch").GetRawText();
                var batch = System.Text.Json.JsonSerializer.Deserialize<Addbatch>(temp);
                var res = await BatchService.AddnewBatch(batch);
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
        [Authorize(Roles = "Mentor,Admin")]
        [DisableRequestSizeLimit]
        [Route("GetAllBatches")]
        public async Task<ActionResult> GetAllBatches([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                int MentorId = data.GetProperty("UserId").GetInt32();
                var res = await BatchService.GetAllBatches(MentorId);
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

        //GetAllBatchesForEmployees

        [HttpPost]
        [Authorize(Roles = "Employee,Mentor,Admin")]
        [DisableRequestSizeLimit]
        [Route("GetAllBatchesForEmployees")]
        public async Task<ActionResult> GetAllBatchesForEmployees([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                int UserId = data.GetProperty("UserId").GetInt32();
                var res = await BatchService.GetAllBatchesForEmployees(UserId);
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
        [Authorize(Roles = "Mentor,Admin")]
        //[AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("AddBatchToUser")]
        public async Task<ActionResult> AddBatchToUser([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {


                var User = data.GetProperty("User").GetRawText();
                AddUserAddUser UserObj = JsonConvert.DeserializeObject<AddUserAddUser>(User);

                int BatchId = data.GetProperty("BatchId").GetInt32();
                var res = await BatchService.AddBatchToUser(UserObj, BatchId);
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
        [Authorize(Roles = "Mentor,Admin")]
        //[AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("GetAllBatch")]
        public async Task<ActionResult> GetAllBatch()//Try [FromBody]
        {
            try
            {



                var res = await BatchService.GetAllBatch();
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
        [Authorize(Roles = "Mentor,Admin")]
        //[AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("RemoveUserFromABatch")]
        public async Task<ActionResult> RemoveUserFromABatch([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {


                var Userid = data.GetProperty("Userid").GetInt32();

                int BatchId = data.GetProperty("BatchId").GetInt32();
                var res = await BatchService.RemoveUSerFromABatch(Userid, BatchId);
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