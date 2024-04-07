using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravkingApplicationAPI.Models;

namespace TravkingApplicationAPI.DTO
{
    public class AddTask
    {
        public int UserTaskID { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public priority Priority { get; set; }
    public DateTime DeadLine { get; set; }
    public int Status { get; set; }
    public int AssignedBy { get; set; }
    public List<int> AssignedTo { get; set; }
    public int BatchId { get; set; }
    public string Comments { get; set; }

    }
}