using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public enum Comments{
         Average,
  VeryGood,
  Good,
  BelowAverage,
  Bad
    }
    public class Rating
    {
     

    public long RatingId { get; set; }
    public int RatedBy { get; set; }//Fk to user (Mentor id)
    
    public int RatedTo { get; set; }//Fk to user id(Employee)
    public int TaskSubmissionId { get; set; }//Fk to submission table
    public int RatingValue { get; set; }
    public Comments Comments { get; set; }//Average,Very Good,Average,Below Good, =>Make the enum
    public int FeedbackId { get; set; }//Fk to Feedback table


    public FeedBack FeedBack { get;set; }//Nav Property

        public TaskSubmissions TaskSubmissions { get;  set; }//Nav Property
        public User RatedByUser { get;  set; }//Nav Property
        public User RatedToUser { get; set; }//Nav Property
    }
}


 