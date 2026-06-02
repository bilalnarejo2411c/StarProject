using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class Hiring
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please select Education")]
        public string Education { get; set; }

        [Required(ErrorMessage = "Please select Profession")]
        public string Profession { get; set; }
    }
}