﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskTracker.Contract.Service;
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

        public string CreateToken(string userId, uint lifetime)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(userId);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims, lifetime);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(_user.Id);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims, 20);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var tokenInfo = _configuration.GetSection("TokenInfo");
            var keyStr = tokenInfo.GetSection("keyString").Value;
            var key = Encoding.UTF8.GetBytes(keyStr);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(string userId)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId)
            };
        }

        private SecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, uint lifetime)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var issuer = jwtSettings.GetSection("validIssuer").Value;
            var audiense = jwtSettings.GetSection("validAudience").Value;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(lifetime)),
                Issuer = issuer,
                Audience = audiense,
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token;
        }

        public Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return System.Threading.Tasks.Task.FromResult(Convert.ToBase64String(randomNumber));
        }
    }
}
