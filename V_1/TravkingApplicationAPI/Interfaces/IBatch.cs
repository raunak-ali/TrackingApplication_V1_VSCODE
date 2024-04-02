using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Interfaces
{
    public interface IBatch
    {
        Task<string> AddnewBatch(Addbatch batch);
                Task<List<Batch>> GetAllBatches(int MentorId);
               


    }
}