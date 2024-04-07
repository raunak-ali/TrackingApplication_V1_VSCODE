using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.DTO
{
    public class AddSubTask
    {
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int TaskId { get; set; }//Fk to Task

    public IFormFile? FileUploadTaskFileUpload{get;set;}

    public byte[]? FileUploadTaskPdf { get; set; }

    public string FileName{get;set;}
    }
}