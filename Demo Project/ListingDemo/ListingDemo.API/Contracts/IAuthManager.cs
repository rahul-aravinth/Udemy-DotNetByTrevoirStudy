using ListingDemo.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ListingDemo.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);
    }
}
