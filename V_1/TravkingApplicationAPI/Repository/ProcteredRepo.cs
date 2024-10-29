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
    public class ProcteredRepo : IProctered
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;


        public ProcteredRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
        }

        public async Task<string> AddnewProct(AddProctered userproc)
        {
            try
            {
                if (userproc != null)
                {
                    Proctered newProcter = new Proctered();
                    newProcter.subtaskid = userproc.subtaskid;
                    newProcter.userId = userproc.userId;
                    newProcter.violations = userproc.violations;
                    context.Proctereds.Add(newProcter);
                    context.SaveChanges();

                    return "Proctor added";
                }
                return null;
            }
            catch (Exception e) { throw; }
        }

        public async Task<string> ApproveProct(int proctid)
        {
            try
            {
                //Okay, remove the Proct row
                var exiting_proc = context.Proctereds.FirstOrDefault(f => f.ProcteredId == proctid);
                if (exiting_proc != null)
                {
                    context.Proctereds.Remove(exiting_proc);
                    context.SaveChanges();
                    return "Porc approved succcesfully";
                }
                return null;

            }
            catch (Exception e) { throw; }

        }

        public async Task<List<GetProctered>> GetAllProctors()
        {
            try
            {
                List<GetProctered> ProctList = new List<GetProctered>();
                var existing_procts = context.Proctereds.ToList();
                if (existing_procts != null)
                {
                    foreach (var proc in existing_procts)
                    {
                        GetProctered newProct = new GetProctered();
                        newProct.proctid=proc.ProcteredId;
                        newProct.userId = proc.userId;//fk

                        newProct.violations = proc.violations;
                        newProct.subtaskid = proc.subtaskid;
                        var existing_user = context.Users.FirstOrDefault(u => u.UserId == proc.userId);
                        newProct.username = existing_user.Name;
                        var existing_subtask = context.SubTask.FirstOrDefault(s => s.SubTaskId == proc.subtaskid);
                        newProct.subtaskname = existing_subtask.Title;
                        var existing_task = context.Tasks.FirstOrDefault(t => t.UserTaskID == existing_subtask.TaskId);
                        newProct.taskname = existing_task.TaskName;
                        var existing_submission = context.TaskSubmissions.FirstOrDefault(tk => tk.subtaskid == proc.subtaskid && tk.UserId == proc.userId);
                        var existing_rating = context.Ratings.FirstOrDefault(r => r.TaskSubmissionId == existing_submission.TaskSubmissionsId && r.RatedTo == proc.userId);

                        newProct.ratingValue = existing_rating.RatingValue;
                        ProctList.Add(newProct);
                    }
                    return ProctList;
                }
                return null;
            }
            catch (Exception e) { throw; }
        }
    }
}