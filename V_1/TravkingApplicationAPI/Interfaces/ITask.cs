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

        Task<string> DeleteTask(int taskid);


        Task<List<Models.UserTask>> GetTaskforUser(int userid);

        Task<List<FeedBack>> GetTaskFeedbacks(int taskid);

        Task<List<string>>GetSubTasksTestCases(int Subtaskid);

        Task<SubTask>GetSubTask(int subtadkid);




    }
}