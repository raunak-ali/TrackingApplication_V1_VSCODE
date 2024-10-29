using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class TaskSubmissionService
    {
        public ITaskSubmissions tasksubrepo;
        public TaskSubmissionService(ITaskSubmissions tasksubrepo)
        {
            this.tasksubrepo = tasksubrepo;
        }
        public async Task<string> GetSubmOfaSubtaskbyUser(int subtaskid, int userid)
        {
            //Used to fetch for a Mentor/Admin

            try
            {
                return await tasksubrepo.GetSubmOfaSubtaskbyUser(subtaskid, userid);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> GetSubmOfaSubtask(int subtaskid)
        {
            //Update the existing subtask with the new EMployee  input
            try
            {
                return await tasksubrepo.GetSubmOfaSubtask(subtaskid);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<string> AddSubmission(AddTaskSubmission taskSubmissions)
        {
            //Update or Add A Rating for a particular SubtAskSubmission

            try
            {

                return await tasksubrepo.AddSubmission(taskSubmissions);
            }
            catch (Exception e)
            {
                throw;
            }


        }
        public async Task<string> RateASubmittedTask(AddRating addRating)
        {


            try
            {
                return await tasksubrepo.RateASubmittedTask(addRating);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> UpdateFeedbacks(List<AddFeedback> feedback)
        {
            try
            {
                return await tasksubrepo.UpdateFeedbacks(feedback);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<string> SendFeedbacktoEmployee(List<AddFeedback> feedback)
        {
            try
            {
                return await tasksubrepo.SendFeedbacktoEmployee(feedback);
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public async Task<string> SendFeedbacktoMentor(List<AddFeedback> feedback)
        {
            try
            {
                return await tasksubrepo.SendFeedbacktoMentor(feedback);

            }
            catch (Exception e) { throw; }
        }

    public async Task<List<FeedBack>> GetFeedbackforaModule(int MouduleId){
         try
            {
                return await tasksubrepo.GetFeedbackforaModule(MouduleId);

            }
            catch (Exception e) { throw; }
    }
    }
    
}