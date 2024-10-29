using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.Repository
{
    public class ModuleRepo: IModule
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;


        public ModuleRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
        }

        public async Task<string> AddNewModuleForBatch(AddModule usermodule)
        {
            try{

                var existing_batch=context.Batches.FirstOrDefault(b=>b.BatchId==usermodule.BatchId);
                if(existing_batch!=null){
                    Models.Module newModule=new Models.Module();
                    newModule.BatchId=usermodule.BatchId;
                    newModule.Description=usermodule.Description;
                    newModule.ModuleName=usermodule.ModuleName;
                    context.Modules.Add(newModule);
                    context.SaveChanges();
                    return "New Module Added to Batch successfully";
                }
                return null;
            }
            catch(Exception e){
                throw;
            }
        }

        public async Task<List<Models.Module>> GetAllModuleForBatch(int BatchId)
        {
            try{
                var existing_batch=context.Batches.FirstOrDefault(b=>b.BatchId==BatchId);
                if(existing_batch!=null){
                    var existing_modules=context.Modules.Where(m=>m.BatchId==BatchId).ToList();
                    existing_modules.ForEach(m => m.Batchs = existing_batch);

                    if(existing_modules!=null){
                        return existing_modules;
                    }
                    return null;
                }
                return null;


            }
            catch(Exception e){
                throw;
            }
        }
    }
}