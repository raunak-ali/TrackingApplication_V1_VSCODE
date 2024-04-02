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
    public class TaskRepo:ITask
    {
        TrackingApplicationDbContext  context;
         static IConfiguration ? _config;

       
        public TaskRepo(DbContextOptions<TrackingApplicationDbContext> options,IConfiguration configuration)
        {
            context=  new TrackingApplicationDbContext(options);
            _config=configuration;
            }

        public async Task<string> AddnewTask(AddTask usertask)
        {
            try{
                
                if (usertask!=null){
                    var existing_mentor=context.Users.FirstOrDefault(ea=>ea.UserId==usertask.AssignedBy);
                    if(existing_mentor!=null){
                        var existing_employee=context.Users.FirstOrDefault(ea=>ea.UserId==usertask.AssignedTo);
                        var existing_batch=context.Batches.FirstOrDefault(b=>b.BatchId==usertask.BatchId);
                        if(existing_employee!=null){
                            UserTask NewTask=new UserTask();
                            NewTask.AssignedBy=usertask.AssignedBy;
                            NewTask.AssignedByUser=existing_mentor;
                            NewTask.AssignedTo=usertask.AssignedTo;
                            NewTask.AssignedToUser=existing_employee;
                            NewTask.BatchId=usertask.BatchId;
                           // NewTask.Batches=existing_batch;
                            NewTask.Comments=usertask.Comments;
                            NewTask.CreatedAt=DateTime.Now;
                            NewTask.DeadLine=usertask.DeadLine;
                            NewTask.Description=usertask.Description;
                            NewTask.Priority=usertask.Priority;
                            NewTask.Status=0;
                            NewTask.TaskName=usertask.TaskName;

                            context.Tasks.Add(NewTask);
                            await context.SaveChangesAsync();
                            return "Changes saved sucessfully";

                        }
                        return "Assigned to Employee does not exist";
                    }
                    return "Mentor does not exist";
                }
                return null;
            }
            catch(Exception ex){
                throw;
            }
        }

        public async Task<List<UserTask>> GetAllTask(int BatchId)
        {
            try{
                if(BatchId!=null){
                    var existing_batch=context.Batches.FirstOrDefault(b=>b.BatchId==BatchId);
                    if(existing_batch!=null){
var all_task=context.Tasks.Where(t=>t.BatchId==BatchId).ToList();
return all_task;

                    }
                }return null;
            }
            catch(Exception e){
                throw;
            }
        }
    }
}