using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.DTOs
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }

}
