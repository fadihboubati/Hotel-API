﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_API.Models.Services
{
    public class JwtTokenService
    {
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _config;
        public JwtTokenService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = configuration;
        }

        // Remember, because GetSecurityKey method is static, so we cant access to any var outside this method, due to that we cant use the _config inside it
        private static SecurityKey GetSecurityKey (IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null) { throw new InvalidOperationException("JWT:Secret is missing"); }
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signinKey = new SymmetricSecurityKey(secretBytes);
            return signinKey;
        }

        // The same note for the configuration
        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),

                
                ValidateIssuer = false,
                ValidateActor = false
            };
        }

        public async Task<string> GetToken(ApplicationUser user, TimeSpan expiresIn)
        {
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            if (principal == null) { return null; }

            var signingKey = GetSecurityKey(_config);

            var token = new JwtSecurityToken(
                  expires: DateTime.UtcNow + expiresIn,
                  signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                  claims: principal.Claims
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
