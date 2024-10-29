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
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using TravkingApplicationAPI.Migrations;


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
                    existing_TaskSubmission.Result = taskSubmissions.Result;
                    existing_TaskSubmission.Test_cases_passed = taskSubmissions.Test_cases_passed;
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
                    var existing_proct=context.Proctereds.Select(p=>p.userId).ToList();
                    foreach (var submission in existing_TaskSubmission)
                    {
                        var existing_rating = context.Ratings
                            .FirstOrDefault(r => r.TaskSubmissionId == submission.TaskSubmissionsId
                            );
                            if(existing_proct.Contains(existing_rating.RatedTo)){
                                existing_rating.RatingValue=0;
                                existing_rating.Comments=Comments.BelowAverage;
                            
                            }

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
                    var ratingThresholds = new Dictionary<double, Comments>
            {
                { 90, Comments.VeryGood },
                { 70, Comments.Good },
                { 50, Comments.Average },
                { 30, Comments.BelowAverage },
                { double.MinValue, Comments.BelowAverage} // Default comment for ratings below 30
            };

                    var existingTaskSubmission = await context.TaskSubmissions.FirstOrDefaultAsync(t => t.TaskSubmissionsId == addRating.TaskSubmissionId);
                    var existingSubTasks = await context.SubTask.Where(s => s.SubTaskId == existingTaskSubmission.subtaskid).ToListAsync();
                    var countOfSubTasks = existingSubTasks.Where(s => s.isProctored == true).Count();
                    var taskID = existingSubTasks.FirstOrDefault().TaskId;
                    existingRating.RatedBy = addRating.RatedBy;
                    existingRating.RatedTo = addRating.RatedTo;
                    existingRating.RatingValue = addRating.RatingValue;
                    existingRating.TaskSubmissionId = addRating.TaskSubmissionId;
                    existingRating.Comments = addRating.Comments;
                    //existingUser.Total_Average_RatingStatus=19;
                    context.Ratings.Update(existingRating);

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

                else
                {
                    var ratingThresholds = new Dictionary<double, Comments>
            {
                { 90, Comments.VeryGood },
                { 70, Comments.Good },
                { 50, Comments.Average },
                { 30, Comments.BelowAverage },
                { double.MinValue, Comments.BelowAverage } // Default comment for ratings below 30
            };

                    var existingTaskSubmission = await context.TaskSubmissions.FirstOrDefaultAsync(t => t.TaskSubmissionsId == addRating.TaskSubmissionId);
                    var existingSubTasks = await context.SubTask.Where(s => s.SubTaskId == existingTaskSubmission.subtaskid).ToListAsync();
                    var countOfSubTasks = existingSubTasks.Count;
                    var taskID = existingSubTasks.FirstOrDefault().TaskId;

                    var newRating = new Rating();

                    newRating.RatedBy = addRating.RatedBy;
                    newRating.RatedTo = addRating.RatedTo;
                    newRating.RatingValue = addRating.RatingValue;
                    newRating.TaskSubmissionId = addRating.TaskSubmissionId;
                    newRating.Comments = addRating.Comments;
                    //existingUser.Total_Average_RatingStatus=19;
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
                                    existing_feedbacks.Comments = (Comments)((int)feed.comments);
                                }
                                else if (feed.comments is string)
                                {
                                    // Handle string values (if needed)
                                    existing_feedbacks.Comments = (Comments)(Convert.ToInt32(feed.comments));
                                }
                                else
                                {
                                    string trimmedComments = feed.comments.ToString().Trim();
                                    if (int.TryParse(trimmedComments, out int commentInt))
                                    {
                                        existing_feedbacks.Comments = (Comments)commentInt;
                                        // Conversion succeeded
                                    }
                                    // Handle other types or unexpected values
                                }
                            }
                            else
                            {
                                // Handle null values (if needed)
                                existing_feedbacks.Comments = existing_feedbacks.Comments;
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

        public async Task<string> SendFeedbacktoEmployee(List<AddFeedback> feedback)
        {
            //Send an email to the Employee about the Feedback on that task
            //fetch the existing User
            //Make a email body for them
            //Send the email using the User profile on Company email. 
            foreach (var feed in feedback)
            {
                var existinguser = context.Users.FirstOrDefault(u => u.UserId == feed.user.userId);
                var email = existinguser.CapgeminiEmailId.Replace("\r", "").Replace("\n", "");
                Comments commentString = Comments.Good;
                if (feed.comments is int)
                {
                    commentString = (Comments)(((int)feed.comments));
                }
                else if (feed.comments is string)
                {
                    // Handle string values (if needed)
                    commentString = (Comments)Convert.ToInt32(feed.comments);
                }
                else
                {
                    string trimmedComments = feed.comments.ToString().Trim();
                    if (int.TryParse(trimmedComments, out int commentInt))
                    {
                        commentString = (Comments)(commentInt);
                        // Conversion succeeded
                    }
                    // Handle other types or unexpected values
                }
                if (commentString == Comments.BelowAverage)
                {

                    SendEmailtoEmployee(email, feed.totalAverageRating.ToString(), existinguser.Name + "|" + feed.userTask.taskName, commentString.ToString(), existinguser.Name, feed.Submission_Count, feed.description);
                }
            }
            return "sent";

        }
        public void SendEmailtoEmployee(string toEmail, string Feedbackvalue, string subject, string feedbackcomment, string employeename, string incomplete_tasks, string feedbackdescription)
        {
            try
            {
                // Set up SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;

                client.Credentials = new NetworkCredential("netasp709@gmail.com", "ndeq qwol oyew bxxr");

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("netasp709@gmail.com");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("<h1>Hello {0}</h1>", employeename);
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<p>I wanted to take a moment to provide you with an update on your progress, discuss the remaining tasks assigned to you, and share feedback based on recent modules assigned to you. </p>");
                mailBody.AppendFormat("<p>Your commitment to growth and development within our dynamic IT company is commendable. As you continue your journey, it's important to address areas for improvement and leverage the support available to you.</p>");
                mailBody.AppendFormat("<p>Regarding your performance, while your dedication to the training program is evident, there are areas where enhancement is needed. During recent assessments, it was noted that you may be facing challenges in fully grasping certain concepts and addressing queries related to the current topics covered. However, with proactive measures and support, we are confident in your ability to overcome these challenges and excel in your role.</p>");
                mailBody.AppendFormat("<p>Moving forward, you have {0} tasks remaining on your agenda.</p>", incomplete_tasks);
                mailBody.AppendFormat("<p>Regarding your feedback, your current feedback value stands at {0} and the comment crrently is at {1}.  The Mentor wants you to Note that {2} .We encourage you to reflect on this feedback and consider it as you continue to grow and develop in your role..</p>", Feedbackvalue, feedbackcomment, feedbackdescription);
                mailBody.AppendFormat("<p>In conclusion, we are here to support you every step of the way. Should you require any assistance or guidance with your remaining tasks or feedback, please do not hesitate to reach out.</p>");
                mailBody.AppendFormat("<p>Wishing you continued success and progress in your professional journey.</p>");
                mailBody.AppendFormat("<p>Warm regards,</p>");
                mailMessage.Body = mailBody.ToString();

                // Send email
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //Lets not throw an exception here since this will not work on office VPN
                Console.WriteLine("ISSUES?");
            }
        }
        public void SendEmailtoMentor(string toEmail, string body, string subject, string employeename)
        {
            try
            {
                // Set up SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;

                client.Credentials = new NetworkCredential("netasp709@gmail.com", "ndeq qwol oyew bxxr");

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("netasp709@gmail.com");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("<h1>Hello {0}</h1>", employeename);
                mailBody.AppendFormat(body.ToString());
                mailMessage.Body = mailBody.ToString();

                // Send email
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //Lets not throw an exception here since this will not work on office VPN
                Console.WriteLine("ISSUES?");
            }
        }
        public async Task<string> SendFeedbacktoMentor(List<AddFeedback> feedback)
        {
            try
            {



                var task = context.Tasks.FirstOrDefault(t => t.UserTaskID == feedback[0].userTask.userTaskID);
                var batch = context.Batches.FirstOrDefault(b => b.BatchId == task.BatchId);
                var mentor = context.Users.FirstOrDefault(m => m.UserId == batch.MentorId);

                // Start building the HTML table
                StringBuilder emphtmlTable = new StringBuilder();
                emphtmlTable.Append("<table border='1'>");

                // Add table headers
                emphtmlTable.Append("<tr>");
                emphtmlTable.Append("<th>Username</th>");
                emphtmlTable.Append("<th>Name</th>");
                emphtmlTable.Append("<th>Capgemini Email ID</th>");
                emphtmlTable.Append("<th> Current Topic :" + task.TaskName + "</th>");
                emphtmlTable.Append("<th> Feedback for  :" + task.TaskName + "</th>");
                emphtmlTable.Append("</tr>");
                foreach (var feed in feedback)
                {
                    //Get the task
                    //Task se batch
                    //batch se mentor
                    //mentor se mentor email


                    //Columns to get of each employee
                    //	Emp ID,Name,Capgemini Email ID,Week 1 and Week 2(Current Topic: C# Programming)
                    //Lets make the employeetable first
                    var existing_user = context.Users.FirstOrDefault(u => u.UserId == feed.user.userId);

                    emphtmlTable.Append("<tr>");
                    emphtmlTable.Append("<td>" + existing_user.UserName + "</td>");
                    emphtmlTable.Append("<td>" + existing_user.Name + "</td>");
                    emphtmlTable.Append("<td>" + existing_user.CapgeminiEmailId + "</td>");
                    Comments commentString = Comments.Good;
                    if (feed.comments is int)
                    {
                        commentString = (Comments)((int)(feed.comments));
                    }
                    else if (feed.comments is string)
                    {
                        // Handle string values (if needed)
                        commentString = (Comments)Convert.ToInt32(feed.comments);
                    }
                    else
                    {
                        string trimmedComments = feed.comments.ToString().Trim();
                        if (int.TryParse(trimmedComments, out int commentInt))
                        {
                            commentString = (Comments)commentInt;
                            // Conversion succeeded
                        }
                        // Handle other types or unexpected values
                    }
                    emphtmlTable.Append("<td>" + commentString + "</td>");
                    emphtmlTable.Append("<td>" + feed.description + "</td>");
                    emphtmlTable.Append("</tr>");


                }

                emphtmlTable.Append("</table>");

                StringBuilder batchhtmlTable = new StringBuilder();
                batchhtmlTable.Append("<table border='1'>");

                // Add table headers
                batchhtmlTable.Append("<tr>");
                batchhtmlTable.Append("<th>Batch Name</th>");
                batchhtmlTable.Append("<th>" + batch.BatchName + "</th>");
                batchhtmlTable.Append("<th>Technology</th>");
                batchhtmlTable.Append("<th>" + batch.Domain + "</th>");
                batchhtmlTable.Append("</tr>");
                batchhtmlTable.Append("<tr>");
                batchhtmlTable.Append("<td> Current Topic </td>");
                batchhtmlTable.Append("<td>" + task.TaskName + "</td>");
                batchhtmlTable.Append("</tr>");





                batchhtmlTable.Append("</table>");
                //Columns to get of the batch

                //Batch Details
                //BatchType:BULT Batch
                //Grading:Excellent,Good,Above Average,Average
                //Batch Name:2024_B06_Jun_DotNet_FullStack_Rashmi
                //Technology:Dot Net Full Stack with React
                //Start Date:02-04-2024.
                //Initial Participants:23
                //Current Topic C# Programming:Below Average
                //Current Batch Pace:Slow,Bad
                //Overall Performance:Good,Above Average,Average,Below Average:   

                try
                {
                    //SendEmail with the table
                    SendEmailtoMentor(mentor.CapgeminiEmailId.Replace("\r", "").Replace("\n", ""), emphtmlTable + "<h3>BATCH DETAILS</h3>" + batchhtmlTable, batch.BatchName, mentor.Name);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Issues", e);
                }
                return "sent";




            }
            catch (Exception e) { throw; }
        }

        public async Task<List<FeedBack>> GetFeedbackforaModule(int MouduleId)
        {
            try
            {
                var ratingThresholds = new Dictionary<double, Comments>
        {
            { 90, Comments.VeryGood },
            { 70, Comments.Good },
            { 50, Comments.Average },
            { 30, Comments.BelowAverage },
            { double.MinValue, Comments.BelowAverage } // Default comment for ratings below 30
        };

                var module =  context.Modules
                    .FirstOrDefault(m => m.ModuleId == MouduleId);

                if (module == null)
                {
                    return null; // Module not found
                }

                var moduleFeedbacks = new List<FeedBack>();

                var batchUserIds = await context.Users
                    .Where(u => u.Batches.Any(b => b.BatchId == module.BatchId))
                    .Select(u => u.UserId)
                    .ToListAsync();

                foreach (var userId in batchUserIds)
                {
                    var existingFeedback = context.FeedBacks
                        .FirstOrDefault(f => f.ModuleId == MouduleId && f.UserId == userId);
                    var task_ids = context.Tasks.Where(t => t.ModuleId == MouduleId).Select(t => t.UserTaskID).ToList();
                    var totaltasks = 0;
                    var Average = 0;
                    foreach (var task in task_ids)
                    {
                        var task_feedbacks = context.FeedBacks.FirstOrDefault(f => f.TaskId == task && f.UserId == userId);
                        if (task_feedbacks != null)
                        {
                            totaltasks = totaltasks + 1;
                            Average = Average + task_feedbacks.TotalAverageRating;
                        }
                    }
                    if (Average != 0 && totaltasks != 0)
                    {
                        Average = Average / totaltasks;
                    }

                    if (existingFeedback != null)
                    {
                        existingFeedback.TotalAverageRating = (int)Average;
                        existingFeedback.User = context.Users.FirstOrDefault(u => u.UserId == userId);
existingFeedback.Comments=ratingThresholds.FirstOrDefault(kv => Average >= kv.Key).Value;
                        context.FeedBacks.Update(existingFeedback);
                        await context.SaveChangesAsync();
                        moduleFeedbacks.Add(existingFeedback);
                    }
                    else
                    {
                        var newFeedback = new FeedBack
                        {
                            UserId = userId,
                            ModuleId = MouduleId,
                            Module = module,
                            TotalAverageRating = (int)Average,
                            Comments = ratingThresholds.FirstOrDefault(kv => Average >= kv.Key).Value,
                            User = context.Users.FirstOrDefault(u => u.UserId == userId)

                        };
                        context.FeedBacks.Add(newFeedback);
                        await context.SaveChangesAsync();
                        moduleFeedbacks.Add(newFeedback);
                    }
                }
                await context.SaveChangesAsync();
                return moduleFeedbacks;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }


        }
    }
}
