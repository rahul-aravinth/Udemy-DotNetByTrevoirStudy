using AutoMapper;
using ListingDemo.API.Contracts;
using ListingDemo.API.Data;
using ListingDemo.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ListingDemo.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomAPIIdentiyUser> _userManager;

        public AuthManager(IMapper mapper, UserManager<CustomAPIIdentiyUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO)
        {
            var user = _mapper.Map<CustomAPIIdentiyUser>(userDTO);
            user.UserName = userDTO.Email;

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return result.Errors;
        }
    }
}
