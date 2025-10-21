using System.ComponentModel.DataAnnotations;

namespace SmartHostelManagementSystem.Models
{
    public class MessMenu
    {
        public int Id { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public string Breakfast { get; set; } = string.Empty;

        [Required]
        public string Lunch { get; set; } = string.Empty;

        [Required]
        public string Dinner { get; set; } = string.Empty;
    }
}