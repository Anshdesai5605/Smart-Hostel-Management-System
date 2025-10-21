using System.ComponentModel.DataAnnotations;

namespace SmartHostelManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public int RoomNo { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}