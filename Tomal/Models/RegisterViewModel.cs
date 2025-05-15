using System.ComponentModel.DataAnnotations;

namespace WorkFinder.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Role { get; set; } // Worker or WorkProvider

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Worker-specific
        public string? Age { get; set; }
        public string? Salary { get; set; }
        public string? Experienced { get; set; }

        // Provider-specific
        public string? WorkerNeed { get; set; }
        public string? ContactNumber { get; set; }

        // Helpers
        public bool IsWorker => Role == "Worker";
        public bool IsWorkProvider => Role == "WorkProvider";
    }
}
