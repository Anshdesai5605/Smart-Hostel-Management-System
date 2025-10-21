using System.ComponentModel.DataAnnotations;

namespace SmartHostelManagementSystem.Models
{
    public class Room
    {
        [Key] // Using RoomNo as the Primary Key
        public int RoomNo { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Display(Name = "Occupied Count")]  
        public int OccupiedCount { get; set; }
    }
}