using Microsoft.AspNetCore.Identity;

namespace ListingDemo.API.Data
{
    public class CustomAPIIdentiyUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
