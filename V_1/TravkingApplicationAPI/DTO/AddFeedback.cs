using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Any;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.DTO
{
    public class AddFeedback
    {
 public int feedbackId { get; set; }
    public int? totalAverageRating { get; set; }
    public object comments { get; set; }
    public string? description { get; set; }
    public UserModel? user { get; set; }
    public TaskModel? userTask { get; set; }
    public Rating?ratings{get;set;}
}

public class UserModel
{
    public int userId { get; set; }
    public string name { get; set; }
    public int total_Average_RatingStatus { get; set; }
}

public class TaskModel
{
    public int userTaskID { get; set; }
    public string taskName { get; set; }
}
}