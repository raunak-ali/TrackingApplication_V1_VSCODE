using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;

namespace TravkingApplicationAPI.Services
{
    public class ProcteredService
    {
        public IProctered proctered;
        public ProcteredService(IProctered proctered)
        {
            this.proctered = proctered;
        }
        public async Task<string> AddnewProct(AddProctered userproc)
        {
            try
            {
                return await proctered.AddnewProct(userproc);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<string> ApproveProct(int proctid)
        {
            try
            {
                return await proctered.ApproveProct(proctid);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<List<GetProctered>> GetAllProctors()
        {
            try
            {
                                return await proctered.GetAllProctors();

            }
            catch (Exception e)
            { throw; }
        }
    }
}