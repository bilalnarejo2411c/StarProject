using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Star_Project.Models

{
    public class AdminVaccanies
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Age{ get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Postiton{ get; set; }
        [Required]
        public string Criteria { get; set; }



    }

}
