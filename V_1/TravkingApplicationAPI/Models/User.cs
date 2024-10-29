using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
  public enum Role
{
    Employee,
    Mentor,
    Admin
}
        public class User
{
       
        public int UserId { get; set; }//PK
    public string Name { get; set; }
    public string UserName { get; set; }//System will geneate on each new registration
    public string Password { get; set; }//System WIll generate on each new registration
    public Role Role { get; set; }
    public string Domain { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }

    public string Phone { get; set; }
    public bool IsCr { get; set; }

     public List<Batch>? Batches { get; set; }=null;//Allows for many to many to employees//Will be null by default 
    public string Gender { get; set; }
    public DateTime Doj { get; set; }

        public string CapgeminiEmailId { get; set; }


    //Only Valid or filled for emlployee 
   
    public string? Grade { get; set; }
   

    public double Total_Average_RatingStatus{get;set;}=0;//Default value starts at 0

    
    public string? PersonalEmailId { get; set; }
    

    public string? EarlierMentorName { get; set; }
    public string? FinalMentorName { get; set; }

    public Comments? FeedbackComment{get;set;}=Comments.BelowAverage;


    public double Attendance_Count{get;set;}=0;//Initailzed to zero by defult

        public List<DailyUpdate>? DailyUpdates { get; set; }=null;
    }
    }
