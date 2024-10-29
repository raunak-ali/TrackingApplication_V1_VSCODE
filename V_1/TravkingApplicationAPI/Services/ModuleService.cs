using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class ModuleService
    {
          public IModule module;
        public ModuleService( IModule module)
        {
            this.module = module;
        }
        

         public async Task<string> AddNewModuleForBatch(AddModule usermodule)
        {
            try
            {
                return await module.AddNewModuleForBatch(usermodule);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Models.Module>> GetAllModuleForBatch(int BatchId)
        {
            try
            {
                return await module.GetAllModuleForBatch(BatchId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}