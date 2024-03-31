using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public class Batch
    {
         public int BatchId { get; set; }//PK
    public string BatchName { get; set; }//System generated using the Date_of_creation+MentorName+Domain
    public User Mentor { get; set; } // Navigation property for the mentor
    public int MentorId { get; set; }//FK to UserID
    public List<User> Employees { get; set; } // Navigation property for the employees
    public string Domain { get; set; }
    public string Description { get; set; }
    public byte[]? AttendanceExcel { get; set; }//Used for File upload

    public byte[]? Employee_info_Excel{get;set;}
    public List<UserTask> UserTask { get; set; }//A Batch has A List of Tasks Attached to it
    }
}