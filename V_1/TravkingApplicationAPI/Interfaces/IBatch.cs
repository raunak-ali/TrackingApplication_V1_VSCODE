using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;

namespace TravkingApplicationAPI.Interfaces
{
    public interface IBatch
    {
        Task<string> AddnewBatch(Addbatch batch);

    }
}