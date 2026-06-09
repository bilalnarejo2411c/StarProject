using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class adminreg
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string UserRole { get; set; } = "user";
    }
}
