using System;

namespace WorkstationManager.Models
{
    public class UserWorkPosition
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkPositionId { get; set; }
        public string ProductName { get; set; } = "";
        public DateTime WorkDate { get; set; }
        public User? User { get; set; }
        public WorkPosition? WorkPosition { get; set; }

    }
}
