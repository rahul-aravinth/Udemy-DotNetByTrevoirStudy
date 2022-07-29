using ListingDemo.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ListingDemo.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);
        Task<AuthResponseDTO> Login(LoginDTO loginDTO);
        Task<string> CreateRefreshToken();
        Task<AuthResponseDTO> VerifyRefershToken(AuthResponseDTO authResponse);
    }
}
