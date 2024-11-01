using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class BatchService
    {
        public IBatch Batch;
        public BatchService(IBatch Batch)
        {
            this.Batch = Batch;
        }

        public async Task<string> AddnewBatch(Addbatch batch)
        {
            try
            {
                return await Batch.AddnewBatch(batch);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Batch>> GetAllBatches(int MentorId)
        {
            try
            {
                return await Batch.GetAllBatches(MentorId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Batch>> GetAllBatchesForEmployees(int UserId)
        {
            try
            {
                return await Batch.GetAllBatchesForEmployees(UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> AddBatchToUser(AddUserAddUser User, int BatchId)
        {
            try
            {
                return await Batch.AddBatchToUser(User, BatchId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Batch>> GetAllBatch()
        {

            try
            {
                return await Batch.GetAllBatch();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<string> RemoveUSerFromABatch(int Userid, int Batchid)
        {

            try
            {
                return await Batch.RemoveUSerFromABatch(Userid, Batchid);

            }
            catch (Exception es) { throw; }
        }

    }

}