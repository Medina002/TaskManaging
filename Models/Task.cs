using AuthenticationProject.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationProject.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime? DueDate { get; set; }

        public string? UserId { get; set; }
        public virtual AuthenticationProjectUser? User { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dueDate)
            {
                return dueDate > DateTime.Now;
            }
            return true;
        }
    }
}
