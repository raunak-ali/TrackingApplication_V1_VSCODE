using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.DTO
{
    public class AddRating
    {
            public int RatedBy { get; set; }//Fk to user (Mentor id)
    
    public int RatedTo { get; set; }//Fk to user id(Employee)
    public int TaskSubmissionId { get; set; }//Fk to submission table
    public int RatingValue { get; set; }
    public Comments Comments { get; set; }
        
    }
}