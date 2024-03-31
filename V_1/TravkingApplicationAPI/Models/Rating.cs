using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public class Rating
    {
     

    public long RatingId { get; set; }
    public int RatedBy { get; set; }
    public int RatedTo { get; set; }
    public int SubTaskId { get; set; }
    public long RatingValue { get; set; }
    public string Comments { get; set; }
    public int FeedbackId { get; set; }
            public FeedBack FeedBack { get; internal set; }//Nav Property

        public SubTask SubTask { get;  set; }//Nav Property
        public User RatedByUser { get;  set; }//Nav Property
        public User RatedToUser { get; set; }//Nav Property
    }
}


 