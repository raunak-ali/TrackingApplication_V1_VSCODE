using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Interfaces
{
    public interface ITask
    {
               Task<string> AddnewTask(AddTask usertask);
                Task<List<UserTask>> GetAllTask(int BatchId);
                
 
    }
}