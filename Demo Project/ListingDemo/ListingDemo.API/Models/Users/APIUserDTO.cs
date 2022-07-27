using System.ComponentModel.DataAnnotations;

namespace ListingDemo.API.Models.Users
{
    public class APIUserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password Limitation {2} to {1} chars", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
