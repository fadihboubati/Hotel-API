using Hotel_API.Models.DTOs;
using Hotel_API.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(RegisterUserDTO data)
        {
            try
            {
                await _userService.Register(data);
                return Ok("Registered successfully");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login (LoginDTO data)
        {
            try
            {
                UserDTO user = await _userService.Authenticate(data);
                return Ok(user);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> Me()
        {
            // Following the [Authorize] phase, this.User will be ... you.
            // Put a breakpoint here and inspect to see what's passed to our getUser method
            return await _userService.GetUser(this.User);
            
        }
    }
}
