﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravkingApplicationAPI.Data;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    [DbContext(typeof(TrackingApplicationDbContext))]
    [Migration("20240415080729_f")]
    partial class f
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
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

                    b.Property<byte[]>("Employee_info_Excel")
                        .HasColumnType("varbinary(max)");

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

                    b.Property<int>("Comments")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TotalAverageRating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.Rating", b =>
                {
                    b.Property<long>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RatingId"));

                    b.Property<int>("Comments")
                        .HasColumnType("int");

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<int>("RatedBy")
                        .HasColumnType("int");

                    b.Property<int>("RatedTo")
                        .HasColumnType("int");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.Property<int>("TaskSubmissionId")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("RatedBy");

                    b.HasIndex("RatedTo");

                    b.HasIndex("TaskSubmissionId");

                    b.ToTable("Ratings");
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

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FileUploadTaskPdf")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubTaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("SubTask");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.TaskSubmissions", b =>
                {
                    b.Property<int>("TaskSubmissionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskSubmissionsId"));

                    b.Property<byte[]>("FileUploadSubmission")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("SubTaskSubmitteddOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("submittedFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("subtaskid")
                        .HasColumnType("int");

                    b.HasKey("TaskSubmissionsId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskSubmissions");
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

                    b.Property<int?>("FeedbackComment")
                        .HasColumnType("int");

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

                    b.Property<int?>("UserTaskID")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserTaskID");

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

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTaskID");

                    b.HasIndex("AssignedBy");

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
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.FeedBack", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.UserTask", "UserTask")
                        .WithMany("FeedBack")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Restrict)
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

                    b.HasOne("TravkingApplicationAPI.Models.TaskSubmissions", "TaskSubmissions")
                        .WithMany()
                        .HasForeignKey("TaskSubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FeedBack");

                    b.Navigation("RatedByUser");

                    b.Navigation("RatedToUser");

                    b.Navigation("TaskSubmissions");
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

            modelBuilder.Entity("TravkingApplicationAPI.Models.TaskSubmissions", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.User", "SubmittedByUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubmittedByUser");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.User", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.UserTask", null)
                        .WithMany("AssignedToUser")
                        .HasForeignKey("UserTaskID");
                });

            modelBuilder.Entity("TravkingApplicationAPI.Models.UserTask", b =>
                {
                    b.HasOne("TravkingApplicationAPI.Models.User", "AssignedByUser")
                        .WithMany()
                        .HasForeignKey("AssignedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravkingApplicationAPI.Models.Batch", null)
                        .WithMany("UserTask")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedByUser");
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
                    b.Navigation("AssignedToUser");

                    b.Navigation("FeedBack");

                    b.Navigation("SubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
