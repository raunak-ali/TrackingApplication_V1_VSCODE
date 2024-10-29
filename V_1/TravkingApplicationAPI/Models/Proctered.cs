using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.Models
{
    public class Proctered
    {
        public int ProcteredId{get;set;}

        public int userId{get;set;}//fk
        public byte[]? violations{get;set;}
        public  int subtaskid{get;set;}//fk
    }
}