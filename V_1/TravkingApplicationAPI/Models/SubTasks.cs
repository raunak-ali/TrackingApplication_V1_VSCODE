using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{

        public class SubTask
{
    public int SubTaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    //public status Status { get; set; }
    public int TaskId { get; set; }//Fk to Task

    public byte[]? FileUploadTaskPdf { get; set; }

     public DateTime CreationDate { get; set; }

     public bool? isCodingProblem{get;set;}=false;
       public string? TestCases{get;set;}=null;

   
   
    
    public UserTask? UserTask { get;set; }//Nav Property

    public string? FileName{get;set;}
    }
    }
