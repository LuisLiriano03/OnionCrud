using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.DTOs
{
    public class CreateUser
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
    }
}
