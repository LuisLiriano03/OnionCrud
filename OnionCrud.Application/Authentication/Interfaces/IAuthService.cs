using OnionCrud.Application.Authentication.DTOs;
using OnionCrud.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(string email, string password);
        Task<GetUser> Register(CreateUser model);
    }
}
