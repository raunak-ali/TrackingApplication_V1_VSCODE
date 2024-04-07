using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Repository
{
    public class TaskRepo : ITask
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;


        public TaskRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
        }

        public async Task<string> AddnewTask(AddTask usertask)
        {
            try
            {

                if (usertask != null)
                {
                    var existing_mentor = context.Users.FirstOrDefault(ea => ea.UserId == usertask.AssignedBy);
                    if (existing_mentor != null)
                    {

                        var assignedToUserIds = usertask.AssignedTo; // Assuming AssignedTo is a List<int>

                        var existing_employee = context.Users
                            .Where(e => assignedToUserIds.Contains(e.UserId))
                            .ToList();
                        var existing_batch = context.Batches.FirstOrDefault(b => b.BatchId == usertask.BatchId);
                        if (existing_employee != null)
                        {
                            UserTask NewTask = new UserTask();
                            NewTask.AssignedBy = usertask.AssignedBy;
                            NewTask.AssignedByUser = existing_mentor;
                            NewTask.AssignedTo = usertask.AssignedTo;
                            NewTask.AssignedToUser = existing_employee;
                            NewTask.BatchId = usertask.BatchId;
                            // NewTask.Batches=existing_batch;
                            NewTask.Comments = usertask.Comments;
                            NewTask.CreatedAt = DateTime.Now;
                            NewTask.DeadLine = usertask.DeadLine;
                            NewTask.Description = usertask.Description;
                            NewTask.Priority = usertask.Priority;
                            NewTask.Status = 0;
                            NewTask.TaskName = usertask.TaskName;

                            context.Tasks.Add(NewTask);
                            await context.SaveChangesAsync();


                        }
                        return "Changes saved sucessfully";

                    }
                    return "Mentor does not exist";
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserTask>> GetAllTask(int BatchId)
        {
            try
            {
                if (BatchId != null)
                {
                    var existing_batch = context.Batches.FirstOrDefault(b => b.BatchId == BatchId);
                    if (existing_batch != null)
                    {
                        var all_task = context.Tasks.Where(t => t.BatchId == BatchId).ToList();
                        return all_task;

                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> AddNewSubtask(AddSubTask subtask)
        {
            try
            {
                if (subtask != null)
                {
                    var existing_Task = context.Tasks.FirstOrDefault(t => t.UserTaskID == subtask.TaskId);
                    if (existing_Task != null)
                    {

                        SubTask newsubtask = new SubTask();
                        newsubtask.CreationDate = DateTime.Now;
                        newsubtask.Description = subtask.Description;
                        newsubtask.FileUploadTaskPdf = subtask.FileUploadTaskPdf;
                        newsubtask.Title = subtask.Title;
                        newsubtask.UserTask = existing_Task;
                        newsubtask.FileName = subtask.FileName;
                        context.SubTask.Add(newsubtask);
                        await context.SaveChangesAsync();


                        //Now lets create a TaskSubmission Object
                        //Make rows with null values ans status as pending for the user's in the AssignedTo part of our task
                        foreach (var Userid in existing_Task.AssignedTo)
                        {
                            TaskSubmissions newtasksub = new TaskSubmissions();
                            newtasksub.UserId = Userid;//Fk to user table
                            newtasksub.subtaskid = newsubtask.SubTaskId;
                            newtasksub.status = status.Pending;
                            newtasksub.submittedFileName = null;
                            newtasksub.FileUploadSubmission = null;
                            newtasksub.SubTaskSubmitteddOn = null;
                            context.TaskSubmissions.Add(newtasksub);
                            await context.SaveChangesAsync();
                            

                        }
                        return "SUBTASK SAVED SUCESSFULLY";


                    }

                }
                return null;
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
                if (TaskId != null)
                {
                    var existing_task = context.Tasks.FirstOrDefault(t => t.UserTaskID == TaskId);
                    if (existing_task != null)
                    {
                        var allsubtask = context.SubTask.Where(s => s.TaskId == TaskId).ToList();
                        if (allsubtask != null && allsubtask.Count > 0)
                        {
                            allsubtask.ForEach(s => s.UserTask = null);
                            return allsubtask;
                        }
                    }
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