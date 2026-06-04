using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models
{
    public class AdminRegistration 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]  
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
         
        [Required]
        public string UserRole{ get; set; }       
    }
}
