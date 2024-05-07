using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public enum status
    {

        Complted,
        Pending,
        Failed_to_submit_within_deadline
    }
    public class TaskSubmissions
    {
        public int TaskSubmissionsId { get; set; }

        public int UserId { get; set; }//Fk to user table

        public int subtaskid { get; set; }
        public status status { get; set; }
        public string? submittedFileName { get; set; }
        public byte[]? FileUploadSubmission { get; set; }

        public string? Result{get;set;}

        public DateTime? SubTaskSubmitteddOn { get; set; }//Date of when The Submission file was submitted

        public User? SubmittedByUser{get;set;}//nav property
         public int? Test_cases_passed{get;set;}

    }
}