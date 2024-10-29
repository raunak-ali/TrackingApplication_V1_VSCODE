using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.DTO
{
    public class GetProctered
    {
        public int proctid{get;set;}
          public int userId{get;set;}//fk
        public byte[]? violations{get;set;}
        public  int subtaskid{get;set;}
    public string username{get;set;}
    public string subtaskname{get;set;}
    public string taskname{get;set;}
    public int ratingValue{get;set;}
    
    }
}