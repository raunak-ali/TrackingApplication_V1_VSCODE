using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
public enum priority{
    low,medium,high
}
        public class UserTask
{
    public int UserTaskID { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public priority Priority { get; set; }//High,Low,medium=>Enum
    public DateTime DeadLine { get; set; }
    public status Status { get; set; }
    public int AssignedBy { get; set; }
    public List<int> AssignedTo { get; set; }//Fk 
    public int BatchId { get; set; }//Fk
    public string Comments { get; set; }

                public int ModuleId { get; set; }
public Module? Module{get;set;}
    public DateTime? CreatedAt { get; set; }
        public User? AssignedByUser { get;set; }//Nav Property
        //public Batch? Batches{get;set;}//Nav Property
        public List<User>? AssignedToUser { get; internal set; }//Nav Property
        public List<SubTask>? SubTasks { get; set; }//Nav Property
        public List<FeedBack>? FeedBack { get; set; }//Nav Property

        //fk of Module
    }
    }
