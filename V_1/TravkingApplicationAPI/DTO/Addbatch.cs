using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.DTO
{
    public class Addbatch
    {
    public int MentorId { get; set; }//FK to UserID
    public string Domain { get; set; }
    public string Description { get; set; }

    public byte[]? Employee_info_Excel{get;set;}

    public IFormFile? Employee_info_Excel_File{ get; set; } // File field for file uploads
    // File field for file uploads
    //This field will later be used to add new Users(role=Employee) and be stored in a byte[] stream.
    //
    }
}
