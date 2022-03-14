using Hotel_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.Interfaces
{
    public interface IUserService
    {
        public Task Register(RegisterUserDTO data);
        public Task<UserDTO> Authenticate(LoginDTO data);
    }
}
