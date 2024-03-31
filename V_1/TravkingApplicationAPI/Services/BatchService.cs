using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;

namespace TravkingApplicationAPI.Services
{
    public class BatchService
    {
        public IBatch Batch;
        public BatchService(IBatch Batch) {
    this.Batch = Batch;
}

       public async Task<string>  AddnewBatch(Addbatch batch){
        try{
            return await Batch.AddnewBatch(batch);
        }
        catch(Exception ex){
            throw;
        }
       }
    }
}