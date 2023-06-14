﻿using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;
using Service.Rapor.Repositories;
using Service.Rapor.Repositories.Interfaces;

namespace Service.Rapor.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRaporRepository, RaporRepository>();
            services.AddScoped<IRaporBilgiRepository, RaporBilgiRepository>();
        }
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RaporContext>(options => options.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")));
        }
        public static void ConfigureVersioning(this IServiceCollection services) => services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        });
        public static void ConfigureVersionedApiExplorer(this IServiceCollection services) =>
            services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
