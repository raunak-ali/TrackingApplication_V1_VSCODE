﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravkingApplicationAPI.Data;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    [DbContext(typeof(TrackingApplicationDbContext))]
    partial class TrackingApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BatchUser", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "BatchId");

                    b.HasIndex("BatchId");

                    b.ToTable("BatchUser");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Batch", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatchId"));

                    b.Property<byte[]>("AttendanceExcel")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("BatchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.HasKey("BatchId");

                    b.HasIndex("MentorId");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.DailyUpdate", b =>
                {
                    b.Property<int>("DailyUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DailyUpdateId"));

                    b.Property<string>("ChallengeToday")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LearnedToday")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OneDriveLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlanForTomorrow")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DailyUpdateId");

                    b.HasIndex("UserId");

                    b.ToTable("DailyUpdates");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.FeedBack", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("TotalAverageRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Rating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RatingId"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<int>("RatedBy")
                        .HasColumnType("int");

                    b.Property<int>("RatedTo")
                        .HasColumnType("int");

                    b.Property<long>("RatingValue")
                        .HasColumnType("bigint");

                    b.Property<int>("SubTaskId")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("RatedBy");

                    b.HasIndex("RatedTo");

                    b.HasIndex("SubTaskId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.SubTask", b =>
                {
                    b.Property<int>("SubTaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubTaskId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FileUploadDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("FileUploadSubmission")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("FileUploadTaskPdf")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubTaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("SubTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<double>("Attendance_Count")
                        .HasColumnType("float");

                    b.Property<string>("CapgeminiEmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Doj")
                        .HasColumnType("datetime2");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EarlierMentorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Employee_info_Excel")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FinalMentorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCr")
                        .HasColumnType("bit");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalEmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<double>("Total_Average_RatingStatus")
                        .HasColumnType("float");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.UserTask", b =>
                {
                    b.Property<int>("UserTaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTaskID"));

                    b.Property<int>("AssignedBy")
                        .HasColumnType("int");

                    b.Property<int>("AssignedTo")
                        .HasColumnType("int");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<long>("Comments")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Priority")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTaskID");

                    b.HasIndex("AssignedBy");

                    b.HasIndex("AssignedTo");

                    b.HasIndex("BatchId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("BatchUser", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.Batch", null)
                        .WithMany()
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Batch", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.User", "Mentor")
                        .WithMany()
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.DailyUpdate", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.User", "User")
                        .WithMany("DailyUpdates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.FeedBack", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.UserTask", "UserTask")
                        .WithOne("FeedBack")
                        .HasForeignKey("TravkingApplicationAPI.Models.FeedBack", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("UserTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Rating", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.FeedBack", "FeedBack")
                        .WithMany("Ratings")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.User", "RatedByUser")
                        .WithMany()
                        .HasForeignKey("RatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.User", "RatedToUser")
                        .WithMany()
                        .HasForeignKey("RatedTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.SubTask", "SubTask")
                        .WithMany()
                        .HasForeignKey("SubTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeedBack");

                    b.Navigation("RatedByUser");

                    b.Navigation("RatedToUser");

                    b.Navigation("SubTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.SubTask", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.UserTask", "UserTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.UserTask", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.User", "AssignedByUser")
                        .WithMany()
                        .HasForeignKey("AssignedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.User", "AssignedToUser")
                        .WithMany()
                        .HasForeignKey("AssignedTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.Batch", "Batches")
                        .WithMany("UserTask")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssignedByUser");

                    b.Navigation("AssignedToUser");

                    b.Navigation("Batches");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Batch", b =>
                {
                    b.Navigation("UserTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.FeedBack", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.User", b =>
                {
                    b.Navigation("DailyUpdates");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.UserTask", b =>
                {
                    b.Navigation("FeedBack")
                        .IsRequired();

                    b.Navigation("SubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
