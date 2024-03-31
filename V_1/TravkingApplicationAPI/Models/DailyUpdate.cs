using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
   
        public class DailyUpdate
{
    public int DailyUpdateId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public DateTime UploadedAt { get; set; }
    public string LearnedToday { get; set; }
    public string PlanForTomorrow { get; set; }
    public string ChallengeToday { get; set; }
    public string OneDriveLink { get; set; }
    public string Summary { get; set; }
        public User User { get; internal set; }
    }
    }
