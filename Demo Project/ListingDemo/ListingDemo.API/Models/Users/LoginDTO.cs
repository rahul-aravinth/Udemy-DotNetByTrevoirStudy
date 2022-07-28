using System.ComponentModel.DataAnnotations;

namespace ListingDemo.API.Models.Users
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Password Limitation {2} to {1} chars", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
