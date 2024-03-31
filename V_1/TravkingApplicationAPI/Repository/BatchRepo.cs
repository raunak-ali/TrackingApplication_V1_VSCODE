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
    public class BatchRepo:IBatch
    {
                 TrackingApplicationDbContext  context;
         static IConfiguration ? _config;

       
        public BatchRepo(DbContextOptions<TrackingApplicationDbContext> options,IConfiguration configuration)
        {
            context=  new TrackingApplicationDbContext(options);
            _config=configuration;
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
            try{
if(batch!=null){
    var existing_mentor=context.Users.FirstOrDefault(s=>s.UserId==batch.MentorId&& s.Role==Role.Mentor);
    
    if(existing_mentor!=null){
        Batch newbatch=new Batch();
newbatch.BatchName=DateTime.Now.Month+"_"+(DateTime.Now.Date)+"_"+existing_mentor.Name+"_"+existing_mentor.Domain;//System generated using the Date_of_creation+MentorName+Domain
newbatch.Description=batch.Description;
newbatch.Domain=batch.Domain;
newbatch.Employee_info_Excel=batch.Employee_info_Excel;
newbatch.Mentor=existing_mentor;
newbatch.MentorId=batch.MentorId;
//Lets save this Batch first
context.Batches.AddAsync(newbatch);
context.SaveChangesAsync();

//Lets use this File_Stream to Add new Employees

if (newbatch.Employee_info_Excel != null || newbatch.Employee_info_Excel.Length != 0){
            

        // Convert byte array to MemoryStream
        using (var stream = new MemoryStream(newbatch.Employee_info_Excel))
        {
            // Extract specific columns from the Excel file
            var extractedData = await ExtractSpecificColumnsAsync(stream);

            // Insert extracted data into the database
            await InsertDataIntoDatabaseAsync(extractedData,newbatch);

            // Return a success message or data if needed
            return "Data inserted successfully";
            
            }
        }

//Now we have a list of employees, We will update the Batch we just added





}

}
return null;

            }
            catch(Exception e){
                throw;
            }
        }

private async Task InsertDataIntoDatabaseAsync(List<List<string>> extractedData, Batch newbatch)
        {
            try{

            var users = new List<User>();
            for(int i = 1; i < extractedData.Count; i++)
            {
                    var rowData = extractedData[i];


                var user = new User
                {
                    Name = rowData[0], // Assuming Name corresponds to the first column
                    UserName=rowData[0].Substring(0,2)+"_"+newbatch.Domain.Substring(0,3),
                    Password=Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(10)).Substring(0, 12),
                    Role=Role.Employee,
                    Domain=newbatch.Domain,
                    JobTitle="Fresher",
                    Location = rowData[1],
                    IsCr=false, 
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

            // Assign newbatch to each user if needed
            foreach (var user in users)
            {

                user.Batches = new List<Batch>(){newbatch};
            }

            // Use Entity Framework to add multiple rows to the database
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    
    catch (Exception e)
    {
        throw;
    }
}
    }
    }
