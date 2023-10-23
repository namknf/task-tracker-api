using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskTracker.Contract;
using TaskTracker.Entities.DataTransferObjects;
using TaskTracker.Entities.Models;

namespace TaskTracker.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<bool> IsValidUser(UserForAuthorizeDto userForAuth)
        {
            _user = await _userManager.FindByEmailAsync(userForAuth.Email);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        }

        public string CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<bool> IsValidNumber(string? number)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => string.Compare(u.PhoneNumber, number) == 0);
            return user != null;
        }

        public async System.Threading.Tasks.Task SendMessageByBot(string? phoneNumber)
        {
            var code = FormRandomCode();
        }

        private static string FormRandomCode()
        {
            var random = new Random();
            var result = random.Next(100000, 999999);
            return result.ToString();
        }

        private SigningCredentials GetSigningCredentials()
        {
            var keyStr = _configuration["TokenSecret"];
            var key = Encoding.UTF8.GetBytes(keyStr);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.Id)
            };
        }

        private SecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var issuer = jwtSettings.GetSection("validIssuer").Value;
            var audiense = jwtSettings.GetSection("validAudience").Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                Issuer = issuer,
                Audience = audiense,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }
    }
}
