using Hotel_API.Models.DTOs;
using Hotel_API.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public async Task Authenticate(LoginDTO data)
        {
            var result = await _signInManager.PasswordSignInAsync(data.Email, data.Password, false, false);
            if (result.Succeeded)
            {
                return;
            }

            throw new Exception("Invalid attempt");
        }

        public async Task Register(RegisterUserDTO data)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = data.Email,
                UserName = data.Email,
                FirstName = data.FirstName,
                LastName = data.LastName
            };

            // Create the user
            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                //sign the user in if it was successful.
                await _signInManager.SignInAsync(user, false);

                return;
            }

            throw new Exception("Oops, Error while trying to register");

        }
    }
}
