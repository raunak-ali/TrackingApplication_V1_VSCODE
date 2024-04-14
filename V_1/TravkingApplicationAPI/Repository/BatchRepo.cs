using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravkingApplicationAPI.Data;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Interfaces;
using TravkingApplicationAPI.Models;
using OfficeOpenXml;

namespace TravkingApplicationAPI.Repository
{
    public class BatchRepo : IBatch
    {
        TrackingApplicationDbContext context;
        static IConfiguration? _config;

        public UserRepo userrepo;


        public BatchRepo(DbContextOptions<TrackingApplicationDbContext> options, IConfiguration configuration)
        {
            context = new TrackingApplicationDbContext(options);
            _config = configuration;
        }
        private async Task<List<List<string>>> ExtractSpecificColumnsAsync(Stream fileStream)
        {
            var extractedData = new List<List<string>>();

            using (var excelPackage = new ExcelPackage(fileStream))
            {
                var worksheet = excelPackage.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                // Modify this list with your specific column names
                var columnsToExtract = new List<string> {
        "Name",
         "Training Location", "Phone No",
         "Gender",
         "DOJ",
         "Capgemini Email ID",
         "Grade",
         "Personal Email ID",
         "Earlier Mentor Name",
         "Final Mentor Name" };

                // Read data in bulk
                var startRow = worksheet.Dimension.Start.Row;
                var endRow = worksheet.Dimension.End.Row;
                var startColumn = worksheet.Dimension.Start.Column;
                var endColumn = worksheet.Dimension.End.Column;

                for (int row = startRow; row <= endRow; row++)
                {
                    var rowData = new List<string>();
                    foreach (var column in columnsToExtract)
                    {
                        var columnIndex = worksheet.Cells[startRow, startColumn, startRow, endColumn].FirstOrDefault(c => c.Value?.ToString() == column)?.Start.Column ?? -1;
                        if (columnIndex != -1)
                        {
                            rowData.Add(worksheet.Cells[row, columnIndex].Value?.ToString() ?? "");
                        }
                        else
                        {
                            // Handle column not found
                            rowData.Add("");
                        }
                    }
                    extractedData.Add(rowData);
                }
            }

            return extractedData;
        }
        public async Task<string> AddnewBatch(Addbatch batch)
        {
            try
            {
                if (batch != null)
                {
                    var existing_mentor = context.Users.FirstOrDefault(s => s.UserId == batch.MentorId);

                    if (existing_mentor != null)
                    {
                        Batch newbatch = new Batch();
                        newbatch.BatchName = DateTime.Now.Month + "_" + (DateTime.Now.Year) + "_" + existing_mentor.Name + "_" + batch.Domain;//System generated using the Date_of_creation+MentorName+Domain
                        newbatch.Description = batch.Description;
                        newbatch.Domain = batch.Domain;
                        newbatch.Employee_info_Excel = batch.Employee_info_Excel;
                        //RE-CHECK THE LOGIC 
                        newbatch.Mentor = existing_mentor;
                        newbatch.MentorId = batch.MentorId;
                        //Lets save this Batch first
                        await context.Batches.AddAsync(newbatch);
                        await context.SaveChangesAsync();

                        //Add the new batch to the existing mentor
                        if (existing_mentor.Batches == null)
                        {
                            existing_mentor.Batches = new List<Batch>();
                        }
                        existing_mentor.Batches.Add(newbatch);
                        await context.SaveChangesAsync();

                        //Lets use this File_Stream to Add new Employees

                        if (newbatch.Employee_info_Excel != null || newbatch.Employee_info_Excel.Length != 0)
                        {


                            // Convert byte array to MemoryStream
                            using (var stream = new MemoryStream(newbatch.Employee_info_Excel))
                            {
                                // Extract specific columns from the Excel file
                                var extractedData = await ExtractSpecificColumnsAsync(stream);

                                // Insert extracted data into the database
                                await InsertDataIntoDatabaseAsync(extractedData, newbatch);

                                // Return a success message or data if needed
                                return "Data inserted successfully";

                            }
                        }

                        //Now we have a list of employees, We will update the Batch we just added





                    }

                }
                return null;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        private async Task InsertDataIntoDatabaseAsync(List<List<string>> extractedData, Batch newbatch)
        {
            try
            {

                var users = new List<User>();
                for (int i = 1; i < extractedData.Count; i++)
                {
                    var rowData = extractedData[i];


                    var user = new User
                    {
                        Name = rowData[0], // Assuming Name corresponds to the first column
                        UserName = rowData[0].Substring(0, 2) + "_" + newbatch.Domain.Substring(0, 3) + "_" + Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(2)).Substring(0, 1),
                        Password = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(10)).Substring(0, 12),
                        Role = Role.Employee,
                        Domain = newbatch.Domain,
                        JobTitle = "Fresher",
                        Location = rowData[1],
                        IsCr = false,
                        Phone = rowData[2],
                        Gender = rowData[3],
                        Doj = DateTime.Parse(rowData[4]),
                        CapgeminiEmailId = rowData[5],
                        Grade = rowData[6],
                        PersonalEmailId = rowData[7],
                        EarlierMentorName = rowData[8],
                        FinalMentorName = rowData[9],
                        // Add more properties as needed, mapping each property to the respective column
                    };
                    users.Add(user);
                    //Send an eamil to Allm these Users indiviually
                }
                //Check if the user with the same personalEmialId Already exixts, if they do just update that User object 


                var usersToAddOrUpdate = new List<User>(); // Create a new list to store users to be added or updated

                // Assign newbatch to each user if needed
                foreach (var user in users)
                {
                    var existingUser = context.Users.FirstOrDefault(u => u.Phone == user.Phone || u.CapgeminiEmailId == user.CapgeminiEmailId);

                    if (existingUser != null)
                    {
                        // Update existing user with new batch
                        if (existingUser.Batches == null)
                        {
                            existingUser.Batches = new List<Batch>();
                        }
                        existingUser.Batches.Add(newbatch);
                        usersToAddOrUpdate.Add(existingUser); // Add existing user with updated batch to the list of users to add or update
                    }
                    else
                    {
                        user.Batches = new List<Batch>() { newbatch }; // Add batch to new user
                        usersToAddOrUpdate.Add(user); // Add new user to the list of users to add or update
                    }
                }

                // Use Entity Framework to add or update users in the database
                foreach (var userToAddOrUpdate in usersToAddOrUpdate)
                {
                    if (userToAddOrUpdate.UserId == 0) // If Id is 0, it's a new user
                    {
                        context.Users.Add(userToAddOrUpdate); // Add new user to the context
                    }
                    else
                    {
                        // If Id is not 0, it's an existing user, Entity Framework will track changes and update the user
                        context.Entry(userToAddOrUpdate).State = EntityState.Modified;
                    }
                }

                await context.SaveChangesAsync();
            } // Save changes to the database

            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<List<Batch>> GetAllBatches(int MentorId)
        {
            try
            {
                var existing_mentor = context.Users.FirstOrDefault(s => s.UserId == MentorId && (s.Role == Role.Mentor || s.Role == Role.Admin));
                if (existing_mentor != null)
                {
                    var existing_batches = context.Batches.Where(b => b.MentorId == MentorId).ToList();
                    if (existing_batches != null && existing_batches.Count > 0)
                    {
                        return existing_batches;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public async Task<List<Batch>> GetAllBatchesForEmployees(int UserId)
        {
            var batchIdsForUser = context.Users
    .Where(u => u.UserId == UserId) // Filter by the specific user ID
    .SelectMany(u => u.Batches.Select(b => b.BatchId)) // Select all BatchIds associated with the user
    .ToList();
            var batchObjectsForUser = context.Batches
            .Where(b => batchIdsForUser.Contains(b.BatchId)) // Filter by the BatchIds
            .ToList();

            //Later on order this according to thier total average rating please
            return batchObjectsForUser;
            //First find the the user object and retirved its Batches property
            //return this batches property.
        }

        public async Task<string>AddBatchToUser(AddUserAddUser User,int BatchId)
        {
            //Check if the User object is already present
            //If yes just update its batches field and do savechanges
            //if Not call the AddUser method, once that one returns Ok, Then add batches to it and savechanges
           try{
var existing_user=context.Users.FirstOrDefault(u=>u.CapgeminiEmailId==User.CapgeminiEmailId);
if(existing_user==null){
//Add a new User

}
var existing_batch=context.Batches.FirstOrDefault(b=>b.BatchId==BatchId);
if(existing_user.Batches==null){
    existing_user.Batches=new List<Batch>();
}
    existing_user.Batches.Add(existing_batch);
    context.SaveChanges();

return"Batch Added to the User sucessfully";
           }
           catch(Exception e){throw;}
        }

        public Task<string> DeleteBatch(int BatchId)
        {
            throw new NotImplementedException();
        }
    }
}
