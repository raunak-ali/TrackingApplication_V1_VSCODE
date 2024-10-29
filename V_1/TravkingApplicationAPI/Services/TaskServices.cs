using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class TaskServices
    {
        public ITask task;
        public TaskServices(ITask task)
        {
            this.task = task;
        }
        public async Task<string> AddnewTask(AddTask usertask)
        {
            try
            {
                return await task.AddnewTask(usertask);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<UserTask>> GetAllTasks(int ModuleId)
        {
            try
            {
                return await task.GetAllTask(ModuleId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<string> AddNewSubtask(AddSubTask subtask)
        {
            try
            {
                return await task.AddNewSubtask(subtask);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<SubTask>> GetAllSubtask(int TaskId)
        {
            try
            {
                return await task.GetAllSubtask(TaskId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<Models.UserTask>> GetTaskforUser(int Userid)
        {
            try
            {
                return await task.GetTaskforUser(Userid);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<string> DeleteTask(int taskid)
        {
            try
            {
                return await task.DeleteTask(taskid);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<FeedBack>> GetTaskFeedbacks(int taskid)
        {
            try
            {
                return await task.GetTaskFeedbacks(taskid);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<List<string>> GetSubTasksTestCases(int subtadkid)
        {

            try
            {
                return await task.GetSubTasksTestCases(subtadkid);
            }
            catch (Exception e) { throw; }
        }
        public async Task<SubTask> GetSubTask(int subtadkid)
        {
            try
            {
                return await task.GetSubTask(subtadkid);
            }
            catch (Exception e) { throw; }
        }



    }
}