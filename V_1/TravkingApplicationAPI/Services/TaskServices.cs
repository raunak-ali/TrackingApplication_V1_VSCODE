using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Services
{
    public class TaskServices
    {
         public ITask task;
        public TaskServices(ITask task) {
    this.task = task;
    }
     public async Task<string>  AddnewTask(AddTask usertask){
        try{
            return await task.AddnewTask(usertask);
        }
        catch(Exception ex){
            throw;
        }
       }
    public async Task<List<UserTask>>  GetAllTasks(int BatchId){
        try{
            return await task.GetAllTask(BatchId);
        }
        catch(Exception ex){
            throw;
        }
       }
    
    
    
    
    }
}