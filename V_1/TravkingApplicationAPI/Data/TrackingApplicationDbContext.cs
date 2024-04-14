using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TravkingApplicationAPI.Models;


namespace TravkingApplicationAPI.Data
{
    public class TrackingApplicationDbContext: DbContext
{
    public TrackingApplicationDbContext(DbContextOptions<TrackingApplicationDbContext> options)
            : base(options){}
    public DbSet<User> Users { get; set; }
    public DbSet<Batch> Batches { get; set; }
//SubTask
        public DbSet<UserTask> Tasks { get; set; }
                public DbSet<SubTask> SubTask { get; set; }
                                public DbSet<FeedBack> FeedBacks { get; set; }
                public DbSet<DailyUpdate> DailyUpdates { get; set; }

                public DbSet<TaskSubmissions> TaskSubmissions{get;set;}

                public DbSet<Rating>Ratings {get;set;}





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the relationship between User and Batch
         modelBuilder.Entity<Batch>()
            .HasOne(b => b.Mentor)                // A Batch has one Mentor (User)
            .WithMany()                           // A User can mentor many Batches
            .HasForeignKey(b => b.MentorId);     // MentorId in Batch is the foreign key

        // Define the many-to-many relationship between Batch and Employees (Users)
       modelBuilder.Entity<User>()
        .HasMany(u => u.Batches)
        .WithMany(b => b.Employees)
        .UsingEntity<Dictionary<string, object>>(
            j => j
                .HasOne<Batch>()
                .WithMany()
                .HasForeignKey("BatchId")
                .OnDelete(DeleteBehavior.Restrict), // Specify the OnDelete behavior
            j => j
                .HasOne<User>()
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Restrict), // Specify the OnDelete behavior
            j =>
            {
                j.HasKey("UserId", "BatchId"); // Define the composite primary key
            });

          // Define the relationship between UserTask and AssignedByUser (User)
    modelBuilder.Entity<UserTask>()
        .HasOne(t => t.AssignedByUser)
        .WithMany()
        .HasForeignKey(t => t.AssignedBy)
        .OnDelete(DeleteBehavior.Restrict);

    

    // Define the relationship between SubTask and UserTask
    modelBuilder.Entity<SubTask>()
        .HasOne(st => st.UserTask)
        .WithMany(t => t.SubTasks)
        .HasForeignKey(st => st.TaskId)
        .OnDelete(DeleteBehavior.Cascade);

    // Define the relationship between FeedBack and UserTask
   // Define the relationship between FeedBack and UserTask
   modelBuilder.Entity<UserTask>()
    .HasMany(t => t.FeedBack) // Assuming FeedBacks is a navigation property in UserTask representing the collection of feedbacks
    .WithOne(f => f.UserTask) // Assuming UserTask is a navigation property in FeedBack representing the parent task
    .HasForeignKey(f => f.TaskId) // Assuming TaskId is the foreign key in FeedBack referencing UserTask
    .OnDelete(DeleteBehavior.Restrict); // Adjust the delete behavior as needed

    // Define the relationship between FeedBack and User
    modelBuilder.Entity<FeedBack>()
        .HasOne(f => f.User)
        .WithMany()
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    // Define the relationship between FeedBack and Rating
    modelBuilder.Entity<FeedBack>()
        .HasMany(f => f.Ratings)
        .WithOne(r => r.FeedBack)
        .HasForeignKey(r => r.FeedbackId)
        .OnDelete(DeleteBehavior.Restrict);

    // Define the relationship between DailyUpdate and User
    modelBuilder.Entity<DailyUpdate>()
        .HasOne(d => d.User)
        .WithMany(u => u.DailyUpdates)
        .HasForeignKey(d => d.UserId)
        .OnDelete(DeleteBehavior.Restrict);
// Define the relationship between Rating and RatedByUser (User)
modelBuilder.Entity<Rating>()
    .HasOne(r => r.RatedByUser)
    .WithMany()
    .HasForeignKey(r => r.RatedBy)
    .OnDelete(DeleteBehavior.Restrict);

// Define the relationship between Rating and RatedToUser (User)
modelBuilder.Entity<Rating>()
    .HasOne(r => r.RatedToUser)
    .WithMany()
    .HasForeignKey(r => r.RatedTo)
    .OnDelete(DeleteBehavior.Restrict);

// Define the relationship between Rating and TaskSubmissions
modelBuilder.Entity<Rating>()
    .HasOne(r => r.TaskSubmissions)
    .WithMany() // Assuming TaskSubmissions can have multiple ratings
    .HasForeignKey(r => r.TaskSubmissionId)
    .OnDelete(DeleteBehavior.Cascade); // Specify the OnDelete behavior if needed

// Define the relationship between Rating and FeedBack
modelBuilder.Entity<Rating>()
    .HasOne(r => r.FeedBack)
    .WithMany(f => f.Ratings)
    .HasForeignKey(r => r.FeedbackId)
    .OnDelete(DeleteBehavior.Restrict);
    base.OnModelCreating(modelBuilder);
}
}
}