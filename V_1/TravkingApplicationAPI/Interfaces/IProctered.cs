using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;

namespace TravkingApplicationAPI.Interfaces
{
    public interface IProctered
    {
                Task<string> AddnewProct(AddProctered userproc);
                //Approving a existing Proct
                Task<string> ApproveProct(int proctid);

                //get all proct for a batch
                Task<List<GetProctered>>GetAllProctors();

    }
}