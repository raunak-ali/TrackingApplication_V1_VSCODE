using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.DTO
{

    public class AddUserAddUser
    {
         public string Name { get; set;}
    public Role Role { get; set; }
    public string Domain { get; set; }
    public string JobTitle { get; set; }
    public string Location { get; set; }

    public string Phone { get; set; }
    public bool IsCr { get; set; }

    public string Gender { get; set; }
    public DateTime Doj { get; set; }

    public string CapgeminiEmailId { get; set; }


    //Only Valid or filled for emlployee 
   
    public string? Grade { get; set; }
   

    public double Total_Average_RatingStatus{get;set;}=0;//Default value starts at 0

    
    public string? PersonalEmailId { get; set; }
    

    public string? EarlierMentorName { get; set; }
    public string? FinalMentorName { get; set; }


    public double Attendance_Count{get;set;}=0;//Initailzed to zero by defult
     public List<Batch>? Batches { get; set; }=null;//Allows for many to many to employees//Will be null by default 

   
    }
}