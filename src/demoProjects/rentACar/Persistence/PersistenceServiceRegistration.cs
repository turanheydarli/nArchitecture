﻿using Application.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseNpgsql(
                configuration.GetConnectionString("PgSql")
                ?? throw new NullReferenceException("Assign connection string in appsettings.json"))
                .EnableSensitiveDataLogging();
            });

            services.AddScoped<IBrandRepository, BrandRepository>();

            return services;
        }
    }
}
