using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Interfaces
{
    public interface ITaskSubmissions{

//Used to fetch for a particular Employee(Fetch the submitted data along with its rating(If provied))
        Task<string> GetSubmOfaSubtaskbyUser(int subtaskid,int userid);
        //Used to fetch for a Mentor/Admin
        Task<string> GetSubmOfaSubtask(int subtaskid);
//Update the existing subtask with the new EMployee  input
        Task <string> AddSubmission(AddTaskSubmission taskSubmissions);
//Update or Add A Rating for a particular SubtAskSubmission
        Task<string> RateASubmittedTask(AddRating addRating);

        Task<List<TaskSubmissions>>GetAllSubmissionsofATask(int taskid,int Userid);
         Task<List<TaskSubmissions>>GetAllSubmissionsofAUser(int Userid);

         Task<string>UpdateFeedbacks(List<AddFeedback> feedback);
//Send Feedback email to individual employees
         Task<string>SendFeedbacktoEmployee(List<AddFeedback>feedback);

//Send Feedback table as an email to the mentor
         Task<string>SendFeedbacktoMentor(List<AddFeedback>feedback);



    }
    }