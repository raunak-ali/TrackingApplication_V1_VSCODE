using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravkingApplicationAPI.DTO
{
    public class AddModule
    {
         //tite
            public string ModuleName { get; set; }
        //description
            public string Description { get; set; }

        //fk of batch
            public int BatchId { get; set; }//Fk
    }
}