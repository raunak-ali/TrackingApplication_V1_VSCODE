using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
   
        public class UserTask
{
    public int UserTaskID { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public long Priority { get; set; }
    public DateTime DeadLine { get; set; }
    public string Status { get; set; }
    public int AssignedBy { get; set; }
    public int AssignedTo { get; set; }
    public int BatchId { get; set; }
    public long Comments { get; set; }
    public DateTime CreatedAt { get; set; }
        public User AssignedByUser { get;set; }//Nav Property
        public Batch Batches{get;set;}//Nav Property
        public User AssignedToUser { get; internal set; }//Nav Property
        public List<SubTask> SubTasks { get; set; }//Nav Property
        public FeedBack FeedBack { get; internal set; }//Nav Property
    }
    }
