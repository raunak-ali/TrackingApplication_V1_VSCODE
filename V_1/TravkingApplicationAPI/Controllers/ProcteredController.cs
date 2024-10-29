using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Services;

namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class ProcteredController : Controller
    {
         private readonly ProcteredService procteredService;

        public ProcteredController(ProcteredService procteredService)
        {
            this.procteredService = procteredService;
        }
         [HttpPost]
        [AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("AddProctered")]
        public async Task<ActionResult> AddProctered([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var temp = data.GetProperty("usermodule");
                int userId = temp.GetProperty("userId").GetInt32();
        int subtaskId = temp.GetProperty("subtaskid").GetInt32();

        // Deserialize violations property into a byte array
        var violationsObject = temp.GetProperty("violations").ToString();;
                byte[] violationsBytes = Convert.FromBase64String(violationsObject);

var userproc = new AddProctered
        {
            userId = userId,
            subtaskid = subtaskId,
            violations = violationsBytes
        };
                var res = await procteredService.AddnewProct(userproc);
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
        [DisableRequestSizeLimit]
        [Route("ApprovProct")]
        public async Task<ActionResult> ApprovProct([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {

                int proctid = data.GetProperty("proctid").GetInt32();
        
                var res = await procteredService.ApproveProct(proctid);
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
        [DisableRequestSizeLimit]
        [Route("GetALlProcts")]
        public async Task<ActionResult> GetALlProcts()//Try [FromBody]
        {
            try
            {

                
        
                var res = await procteredService.GetAllProctors();
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