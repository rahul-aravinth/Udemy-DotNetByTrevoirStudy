using System.ComponentModel.DataAnnotations;

namespace ListingDemo.API.Models.Users
{
    public class APIUserDTO : LoginDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
