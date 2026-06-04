using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class Hiring
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Education { get; set; }

        [Required]
        public string Profession { get; set; }
    }
}