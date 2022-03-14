using Hotel_API.Data;
using Hotel_API.Models.DTOs;
using Hotel_API.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private AppDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        public async Task<UserDTO> Authenticate(LoginDTO data)
        
        {
            //_context.Users, _context.Roles, _context.UserRoles, and more ...
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == data.Email);
            var result = await _signInManager.PasswordSignInAsync(user.UserName, data.Password, false, false);
            if (result.Succeeded)
            {
                UserDTO userDto = new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                return userDto;
            }

            throw new Exception("Invalid attempt");
        }

        public async Task Register(RegisterUserDTO data)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = data.UserName,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email
            };

            // Create a new user (new record) in the AspNetUsers table
            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return;
            }

            throw new Exception("Oops, Error while trying to register");

        }
    }
}
