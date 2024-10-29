using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;

namespace TravkingApplicationAPI.Models
{
    public class Module
    {
        //fk
            public int ModuleId { get; set; }

        //tite
            public string ModuleName { get; set; }
        //description
            public string Description { get; set; }

        //fk of batch
            public int BatchId { get; set; }//Fk

            public Batch? Batchs{get;set;}

        
    }
}