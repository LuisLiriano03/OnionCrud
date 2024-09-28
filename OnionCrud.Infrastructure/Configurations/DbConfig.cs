using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionCrud.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Infrastructure.Configurations
{
    public static class DbConfig
    {
        public static IServiceCollection ConfigDbConnection(this IServiceCollection service, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("myConnection")!;
            service.AddDbContext<DbOnionCrudContext>(options => options.UseSqlServer(connectionString));

            return service;
        }

    }
}
