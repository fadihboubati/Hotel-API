using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
