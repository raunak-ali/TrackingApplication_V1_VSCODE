using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                        var existing_user = context.Users.FirstOrDefault(u => u.UserId == submission.UserId);
                        submission.SubmittedByUser = existing_user;

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
                    var existing_user = context.Users.FirstOrDefault(u => u.UserId == existing_TaskSubmission.UserId);
                    existing_TaskSubmission.SubmittedByUser = existing_user;
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
                var existingRating = await context.Ratings.FirstOrDefaultAsync(r => r.TaskSubmissionId == addRating.TaskSubmissionId);

                if (existingRating != null)
                {
                    return "This TaskSubmission has already been rated.";
                }
                else
                {
                    var ratingThresholds = new Dictionary<double, Comments>
            {
                { 90, Comments.VeryGood },
                { 70, Comments.Good },
                { 50, Comments.Average },
                { 30, Comments.BelowAverage },
                { double.MinValue, Comments.Bad } // Default comment for ratings below 30
            };

                    var existingTaskSubmission = await context.TaskSubmissions.FirstOrDefaultAsync(t => t.TaskSubmissionsId == addRating.TaskSubmissionId);
                    var existingSubTasks = await context.SubTask.Where(s => s.SubTaskId == existingTaskSubmission.subtaskid).ToListAsync();
                    var countOfSubTasks = existingSubTasks.Count;
                    var taskID = existingSubTasks.FirstOrDefault().TaskId;

                    var existingUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == addRating.RatedTo);
                    var countTasksAssignedToUser = await context.Tasks.Where(t => t.AssignedTo.Contains(addRating.RatedTo)).CountAsync();

                    var feedback = await context.FeedBacks.FirstOrDefaultAsync(f => f.TaskId == taskID && f.UserId == addRating.RatedTo);
                    var isNewFeedback = feedback == null;

                    if (isNewFeedback)
                    {
                        feedback = new FeedBack();
                        feedback.TotalAverageRating = (1 / countOfSubTasks) * addRating.RatingValue;
                        feedback.TaskId = taskID;
                        feedback.UserId = addRating.RatedTo;
                        feedback.Comments = ratingThresholds.FirstOrDefault(kv => feedback.TotalAverageRating >= kv.Key).Value;
                        await context.FeedBacks.AddAsync(feedback);
                    }
                    else
                    {
                        feedback.TotalAverageRating = ((1 / countOfSubTasks) * addRating.RatingValue) + feedback.TotalAverageRating;
                        feedback.Comments = ratingThresholds.FirstOrDefault(kv => feedback.TotalAverageRating >= kv.Key).Value;
                        context.FeedBacks.Update(feedback);

                    }

                    var newRating = new Rating();

                    newRating.RatedBy = addRating.RatedBy;
                    newRating.RatedTo = addRating.RatedTo;
                    newRating.RatingValue = addRating.RatingValue;
                    newRating.TaskSubmissionId = addRating.TaskSubmissionId;
                    newRating.Comments = addRating.Comments;
                    existingUser.Total_Average_RatingStatus = ((1 / 1) * feedback.TotalAverageRating) + existingUser.Total_Average_RatingStatus;
                    //existingUser.Total_Average_RatingStatus=19;
                    context.Users.Update(existingUser);
                    await context.Ratings.AddAsync(newRating);

                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            // Perform database operations within the transaction

                            // Save changes to the database
                            await context.SaveChangesAsync();

                            // Commit the transaction if all operations succeed
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            // Rollback the transaction if an error occurs
                            await transaction.RollbackAsync();
                            throw; // Rethrow the exception for logging or handling elsewhere
                        }
                    }

                    newRating.FeedbackId = feedback.FeedbackId;
                    using (var transaction = await context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            // Perform database operations within the transaction

                            // Save changes to the database
                            await context.SaveChangesAsync();

                            // Commit the transaction if all operations succeed
                            await transaction.CommitAsync();
                        }
                        catch (Exception)
                        {
                            // Rollback the transaction if an error occurs
                            await transaction.RollbackAsync();
                            throw; // Rethrow the exception for logging or handling elsewhere
                        }
                    }

                    return "SubTask has been rated.";
                }
            }
            catch (Exception e)
            {
                throw; // Rethrow the exception for logging or handling elsewhere
            }

        }

        public async Task<List<TaskSubmissions>> GetAllSubmissionsofATask(int taskid, int Userid)
        {
            var existing_user = context.Users.FirstOrDefault(u => u.UserId == Userid);
            var existing_task = context.Tasks.FirstOrDefault(t => t.UserTaskID == taskid);
            if (existing_user != null && existing_task != null)
            {
                var existing_subtasks = context.SubTask.Where(s => s.TaskId == taskid).ToList();
                List<TaskSubmissions> tasksub = new List<TaskSubmissions>();
                foreach (var subtask in existing_subtasks)
                {
                    var existing_submission = context.TaskSubmissions.Where(t => t.subtaskid == subtask.SubTaskId).ToList();
                    tasksub.Concat(existing_submission);
                }
                return tasksub;

            }
            return null;

        }

        public async Task<List<TaskSubmissions>> GetAllSubmissionsofAUser(int Userid)
        {

            try
            {
                var existing_user = context.Users.FirstOrDefault(u => u.UserId == Userid);
                if (existing_user != null)
                {
                    var existing_submissions = context.TaskSubmissions.Where(t => t.UserId == Userid).ToList();
                    return existing_submissions;
                }
                return null;
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
                if (feedback != null)
                {
                    foreach (var feed in feedback)
                    {
                        var existing_feedbacks = await context.FeedBacks.FirstOrDefaultAsync(f => f.FeedbackId == feed.feedbackId);
                        if (existing_feedbacks != null)
                        {
                            if (feed.comments != null)
                            {
                                if (feed.comments is int)
                                {
                                    existing_feedbacks.Comments = (Comments)(int)feed.comments;
                                }
                                else if (feed.comments is string)
                                {
                                    // Handle string values (if needed)
                                    existing_feedbacks.Comments = (Comments)Convert.ToInt32(feed.comments);
                                }
                                else
                                {
                                  string trimmedComments = feed.comments.ToString().Trim();
if (int.TryParse(trimmedComments, out int commentInt))
{
    existing_feedbacks.Comments=(Comments)commentInt;
    // Conversion succeeded
}
                                    // Handle other types or unexpected values
                                }
                            }
                            else
                            {
                                // Handle null values (if needed)
                                existing_feedbacks.Comments =existing_feedbacks.Comments;
                            }


                            existing_feedbacks.Description = feed.description;
                            existing_feedbacks.TotalAverageRating = (int)feed.totalAverageRating;
                            context.FeedBacks.Update(existing_feedbacks);
                        }
                    }
                    context.SaveChanges();
                }
                return null;
            }
            catch (Exception e) { throw; }
        }
    }
}