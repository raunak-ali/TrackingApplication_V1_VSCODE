using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;
using Module = TravkingApplicationAPI.Models.Module;

namespace TravkingApplicationAPI.Interfaces
{
    public interface IModule
    {
         Task<string> AddNewModuleForBatch(AddModule usermodule);
        Task<List<Module>> GetAllModuleForBatch(int BatchId);

    }
}