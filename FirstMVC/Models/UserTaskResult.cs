using System;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class UserTaskResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public int TaskId { get; set; }

        [Required]
        public int ActNumber { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
