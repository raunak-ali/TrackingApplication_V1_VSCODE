using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Repository
{
    public class TaskSubmissionsRepo : ITaskSubmissions
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;


        public TaskSubmissionsRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
        }


        public async Task<string> AddSubmission(AddTaskSubmission taskSubmissions)
        {
            try
            {
                //Update the existing Submission object && //Add a new Empty Ratiing Object
                var existing_TaskSubmission = context.TaskSubmissions.FirstOrDefault(t => t.subtaskid == taskSubmissions.subtaskid && t.UserId == taskSubmissions.UserId);
                if (existing_TaskSubmission != null)
                {
                    existing_TaskSubmission.submittedFileName = taskSubmissions.submittedFileName;
                    existing_TaskSubmission.FileUploadSubmission = taskSubmissions.FileUploadSubmission;
                    existing_TaskSubmission.status = status.Complted;
                    existing_TaskSubmission.SubTaskSubmitteddOn = DateTime.Now;
                    //Add a updated one now;
                    //context.TaskSubmissions.Add(existing_TaskSubmission);
                    context.SaveChanges();
                    return "Submission saved sucessfully";

                }
                return null;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> GetSubmOfaSubtask(int subtaskid)
        {
            //Since this is expected to return a Json of the two object the TaskSubmitted data
            // and the rating of it if found So return type is a string

            try
            {

                var existing_TaskSubmission = context.TaskSubmissions.Where(s => s.subtaskid == subtaskid).ToList();
                
                List<object> response = new List<object>();

                if (existing_TaskSubmission != null)
                {
                    foreach (var submission in existing_TaskSubmission)
                    {
                        var existing_rating = context.Ratings
                            .FirstOrDefault(r => r.TaskSubmissionId == submission.TaskSubmissionsId
                            );
var existing_user=context.Users.FirstOrDefault(u=>u.UserId==submission.UserId);
submission.SubmittedByUser=existing_user;

                        // Create an object to hold submission details and rating
                        var submissionData = new
                        {

                            SubmissionDetails = submission, // You can include additional submission details here
                            Rating = existing_rating // Assuming Rating is the property you want to include
                        };

                        response.Add(submissionData);

                    }
                }

                var jsonResponse = JsonConvert.SerializeObject(response);
                return jsonResponse;
            }


            catch (Exception e)
            {
                throw;
            }


        }

        public async Task<string> GetSubmOfaSubtaskbyUser(int subtaskid, int userid)
        {
            try
            {
                var existing_TaskSubmission = context.TaskSubmissions.FirstOrDefault(s => s.subtaskid == subtaskid && s.UserId == userid);

                if (existing_TaskSubmission != null)
                {
                    var existing_Rating = context.Ratings.FirstOrDefault(r => r.TaskSubmissionId == existing_TaskSubmission.TaskSubmissionsId);
                    var existing_user=context.Users.FirstOrDefault(u=>u.UserId==existing_TaskSubmission.UserId);
existing_TaskSubmission.SubmittedByUser=existing_user;
                    var submissionData = new
                    {

                        SubmissionDetails = existing_TaskSubmission, // You can include additional submission details here
                        Rating = existing_Rating // Assuming Rating is the property you want to include
                    };
                    var jsonResponse = JsonConvert.SerializeObject(submissionData);
                    return jsonResponse;
                }
                else
                {
                    var submissionData = new
                    {

                        SubmissionDetails = existing_TaskSubmission, // You can include additional submission details here
                        Rating = "" // Assuming Rating is the property you want to include
                    };
                    var jsonResponse = JsonConvert.SerializeObject(submissionData);
                    return jsonResponse;
                }






                return null;
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
                var existing_rating = context.Ratings.FirstOrDefault(r => r.TaskSubmissionId == addRating.TaskSubmissionId);
                if (existing_rating != null)
                {
                    //Check if rating for that tasksubmission for that user already exixts if it does return that object

                    return "This TaskSubmission Already Rated";
                }
                else
                {
                    //Check if a feedback object for that task already exists
                    //Get TaskSubmissedId=>SubTaskId=>TaskId=>Feedback Table 
                    //If yes update that object
                    //Else Create a new Feedback row
                    var existing_Task_submission = context.TaskSubmissions.FirstOrDefault(t => t.TaskSubmissionsId == addRating.TaskSubmissionId);
                    var existing_Sub_task = context.SubTask.Where(s => s.SubTaskId == (existing_Task_submission.subtaskid));
                    var count_ofsubtasks = existing_Sub_task.Count();
                    var Add = (1 / count_ofsubtasks) * 100;
                    var taskid = existing_Sub_task.FirstOrDefault().TaskId;


                    var feed = 1;
                    var existing_feedback = context.FeedBacks.FirstOrDefault(
                        f => f.TaskId == taskid && f.UserId==addRating.RatedTo);
                        
                    if (existing_feedback != null)
                    {
                        feed = existing_feedback.FeedbackId;





                        existing_feedback.TotalAverageRating = ((1 / count_ofsubtasks) * addRating.RatingValue) + existing_feedback.TotalAverageRating;
                        
                        //Update the Total Average rating of that user as well 
                        var existing_user=context.Users.FirstOrDefault(u=>u.UserId==addRating.RatedTo);
                        var count_tasks_assigned_to_user=context.Tasks
    .Where(t => t.AssignedTo != null && t.AssignedTo.Contains(addRating.RatedTo))
    .Count();


                        existing_user.Total_Average_RatingStatus=((1 / count_tasks_assigned_to_user) *  existing_feedback.TotalAverageRating) + existing_user.Total_Average_RatingStatus;
                        
                        var ratingThresholds = new Dictionary<double, Comments>
{
    { 90, Comments.VeryGood },
    { 70, Comments.Good },
    { 50, Comments.Average },
    { 30, Comments.BelowAverage },
    { double.MinValue, Comments.Bad } // Default comment for ratings below 30
};

                        // Determine the comment based on the updated total average rating
                        existing_feedback.Comments = ratingThresholds.FirstOrDefault(kv => existing_feedback.TotalAverageRating >= kv.Key).Value;
                        context.SaveChanges();

                    }
                    else
                    {
                        FeedBack feedback = new FeedBack();
                        feedback.TotalAverageRating = (1 / count_ofsubtasks) * addRating.RatingValue;
                        feedback.TaskId = taskid;
                        feedback.UserId = addRating.RatedTo;
                        feedback.Comments = Comments.Bad;

                        context.FeedBacks.Add(feedback);
                        context.SaveChanges();
                        feed = feedback.FeedbackId;


                    }
                    Rating newrating = new Rating();
                    newrating.FeedbackId = feed;
                    newrating.RatedBy = addRating.RatedBy;
                    newrating.RatedTo = addRating.RatedTo;
                    newrating.RatingValue = addRating.RatingValue;
                    newrating.TaskSubmissionId = addRating.TaskSubmissionId;
                    newrating.Comments = addRating.Comments;
                    context.Ratings.Add(newrating);
                    context.SaveChanges();
                    return "SubTask rated";

                }

                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}