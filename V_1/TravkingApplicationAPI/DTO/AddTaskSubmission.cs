using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.DTO
{
    public class AddTaskSubmission
    {
     public int UserId{get;set;}//Fk to user table

        public int subtaskid{get;set;}
public status status{get;set;}=status.Complted;
public string? submittedFileName{get;set;}
public IFormFile? FileUpload{get;set;}//Will always be null
        public string Result{get;set;}

         public byte[]? FileUploadSubmission { get; set; }
    public DateTime? SubTaskSubmitteddOn { get; set; }//Date of when The Submission file was submitted   

    public int? Test_cases_passed{get;set;}
    }
}