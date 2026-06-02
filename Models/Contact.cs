using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? Company { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? Phone { get; set; }

        public string? Service { get; set; }

        [Required]
        public string Message { get; set; }
    }
}