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

                Task<string> AddNewSubtask(AddSubTask subtask);

                Task<List<SubTask>> GetAllSubtask(int TaskId);

                
Task<List<Models.UserTask>> GetTaskforUser(int userid);

                
                
 
    }
}