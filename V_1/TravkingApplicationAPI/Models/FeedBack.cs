using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public class FeedBack
    {
    
    public int FeedbackId { get; set; }

    
    public int TaskId { get; set; }
    public int TotalAverageRating { get; set; }

    
    public Comments Comments { get; set; }

        public string? Description { get; set; }=null;
    public int UserId { get; set; }
    public UserTask? UserTask { get; set; } // Navigation property for Task
    public User? User { get; set; } // Navigation property for User
    public List<Rating>? Ratings { get; set; } // Navigation property for Ratings
public string? Submission_Count { get; set; }=null;
    }



    }
