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
    public class ModuleController : Controller
    {
        private readonly ModuleService ModuleService;

        public ModuleController(ModuleService ModuleService)
        {
            this.ModuleService = ModuleService;
        }
        [HttpPost]
        [AllowAnonymous]
        [DisableRequestSizeLimit]
        [Route("AddModule")]
        public async Task<ActionResult> AddNewModuleForBatch([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var temp = data.GetProperty("usermodule").GetRawText();
                var usermodule = System.Text.Json.JsonSerializer.Deserialize<AddModule>(temp);

                var res = await ModuleService.AddNewModuleForBatch(usermodule);
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
        [Route("GetAllModules")]
        public async Task<ActionResult> GetAllModuleForBatch([FromBody] dynamic data)//Try [FromBody]
        {
            try
            {
                var BatchId = data.GetProperty("batchid").GetInt32();

                var res = await ModuleService.GetAllModuleForBatch(BatchId);
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