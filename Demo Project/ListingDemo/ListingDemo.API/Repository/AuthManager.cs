using AutoMapper;
using ListingDemo.API.Contracts;
using ListingDemo.API.Data;
using ListingDemo.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ListingDemo.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomAPIIdentiyUser> _userManager;
        private readonly IConfiguration _configuration;
        private CustomAPIIdentiyUser _user;

        private const string _loginProvider = "ListingDemoAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<CustomAPIIdentiyUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO)
        {
            _user = _mapper.Map<CustomAPIIdentiyUser>(userDTO);
            _user.UserName = userDTO.Email;

            var result = await _userManager.CreateAsync(_user, userDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

            return result.Errors;
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWTSettings:Issuer"],
                audience: _configuration["JWTSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JWTSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {
            _user = await _userManager.FindByEmailAsync(loginDTO.Email);
            bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDTO.Password);
            
            if (_user == null || !isValidUser)
            {
                return null;
            }

            var token = await GenerateToken();

            return new AuthResponseDTO
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDTO> VerifyRefershToken(AuthResponseDTO authResponse)
        {
            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityHandler.ReadJwtToken(authResponse.Token);
            var userName = tokenContent.Claims.ToList().FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
            _user = await _userManager.FindByNameAsync(userName);

            if(_user == null || _user.Id != authResponse.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, authResponse.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDTO
                {
                    UserId = _user.Id,
                    Token = token,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            await _userManager.UpdateSecurityStampAsync(_user);

            return null;
        }
    }
}
