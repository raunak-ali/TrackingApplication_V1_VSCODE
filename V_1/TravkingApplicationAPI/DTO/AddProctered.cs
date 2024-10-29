using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.DTO
{
    public class AddProctered
    {
        public int userId{get;set;}//fk
        public byte[]? violations{get;set;}
        public  int subtaskid{get;set;}

    }
}