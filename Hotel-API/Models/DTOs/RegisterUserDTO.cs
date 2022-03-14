using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API.Models.DTOs
{
    public class RegisterUserDTO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
