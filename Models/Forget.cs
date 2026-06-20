using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class Forget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }
}