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
[Authorize(Roles ="Admin")]
[DisableRequestSizeLimit]
[Route("AddBatch")]
public async Task<ActionResult>  AddBatch([FromBody]dynamic data)//Try [FromBody]
{
try{ 
    var temp=data.GetProperty("batch").GetRawText();
    var batch = JsonSerializer.Deserialize<Addbatch>(temp);
    var res = await BatchService.AddnewBatch(batch);
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
[Authorize(Roles ="Mentor")]
[DisableRequestSizeLimit]
[Route("GetAllBatches")]
public async Task<ActionResult>  GetAllBatches([FromBody]dynamic data)//Try [FromBody]
{
    try{
int MentorId=data.GetProperty("MentorId").GetInt32();
        var res = await BatchService.GetAllBatches(MentorId);
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