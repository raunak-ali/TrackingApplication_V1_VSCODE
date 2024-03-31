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

         modelBuilder.Entity<UserTask>()
        .HasOne(t => t.AssignedByUser)
        .WithMany()
        .HasForeignKey(t => t.AssignedBy)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior

    modelBuilder.Entity<UserTask>()
        .HasOne(t => t.AssignedToUser)
        .WithMany()
        .HasForeignKey(t => t.AssignedTo)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior

    modelBuilder.Entity<UserTask>()
        .HasOne(t => t.Batches)
        .WithMany(b => b.UserTask)
        .HasForeignKey(t => t.BatchId)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior

    // Define relationship between Task and SubTask
    modelBuilder.Entity<SubTask>()
        .HasOne(st => st.UserTask)
        .WithMany(t => t.SubTasks)
        .HasForeignKey(st => st.TaskId)
        .OnDelete(DeleteBehavior.Cascade); // Specify the OnDelete behavior if needed

    // Define relationship between Rating and User for RatedBy
    modelBuilder.Entity<Rating>()
        .HasOne(r => r.RatedByUser)
        .WithMany()
        .HasForeignKey(r => r.RatedBy)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior if needed

    // Define relationship between Rating and User for RatedTo
    modelBuilder.Entity<Rating>()
        .HasOne(r => r.RatedToUser)
        .WithMany()
        .HasForeignKey(r => r.RatedTo)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior if needed

    // Define relationship between Rating and SubTask
    modelBuilder.Entity<Rating>()
        .HasOne(r => r.SubTask)
        .WithMany()
        .HasForeignKey(r => r.SubTaskId)
        .OnDelete(DeleteBehavior.Cascade); // Specify the OnDelete behavior if needed
// Define relationship between Feedback and Task
    modelBuilder.Entity<FeedBack>()
        .HasOne(f => f.UserTask) // Specify the navigation property for Task in Feedback
        .WithOne(t => t.FeedBack) // Specify the navigation property for Feedback in Task
        .HasForeignKey<FeedBack>(f => f.TaskId) // Specify the foreign key property in Feedback
        .OnDelete(DeleteBehavior.Cascade);// Specify the OnDelete behavior if needed

    // Define relationship between Feedback and User
    modelBuilder.Entity<FeedBack>()
        .HasOne(f => f.User)
        .WithMany()
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Restrict); // Specify the OnDelete behavior if needed

    // Define relationship between Feedback and Rating (assuming one-to-many)
    modelBuilder.Entity<FeedBack>()
        .HasMany(f => f.Ratings)
        .WithOne(f=>f.FeedBack) // Assuming there's no navigation property in Rating pointing back to Feedback
        .HasForeignKey(r => r.FeedbackId)
.OnDelete(DeleteBehavior.Restrict); 

     // Define the relationship between DailyUpdate and User
    modelBuilder.Entity<DailyUpdate>()
        .HasOne(d => d.User)                    // DailyUpdate has one User
        .WithMany(u => u.DailyUpdates)          // User can have many DailyUpdates
        .HasForeignKey(d => d.UserId); 




        base.OnModelCreating(modelBuilder);

    }
}
}