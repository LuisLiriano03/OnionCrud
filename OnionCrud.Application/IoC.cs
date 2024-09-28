using Microsoft.Extensions.DependencyInjection;
using OnionCrud.Application.Authentication.Interfaces;
using OnionCrud.Application.Authentication.Services;
using OnionCrud.Application.Users.Interfaces;
using OnionCrud.Application.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application
{
    public static class IoC
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            return service
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>();



        }

    }
}
