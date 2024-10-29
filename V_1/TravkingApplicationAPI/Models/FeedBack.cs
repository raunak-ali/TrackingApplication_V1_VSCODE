using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
   // In the feedback table the ModuleID is only populated for rows which correspond to the moduleid only 
   //and for these the taskid is null
    public class FeedBack
    {
    
    public int FeedbackId { get; set; }

    
    public int? TaskId { get; set; }
    public int TotalAverageRating { get; set; }

    
    public Comments Comments { get; set; }

        public string? Description { get; set; }=null;
    public int UserId { get; set; }
    public UserTask? UserTask { get; set; } // Navigation property for Task
    public User? User { get; set; } // Navigation property for User
    public List<Rating>? Ratings { get; set; } // Navigation property for Ratings
public string? Submission_Count { get; set; }=null;
     public Module? Module { get; set; }

public int? ModuleId{get;set;}
    }



    }
