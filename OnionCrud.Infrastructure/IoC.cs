using Microsoft.Extensions.DependencyInjection;
using OnionCrud.Domain.Interfaces;
using OnionCrud.Infrastructure.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            return service
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
