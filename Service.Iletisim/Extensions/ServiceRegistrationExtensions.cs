using Microsoft.EntityFrameworkCore;
using Service.Iletisim.Data;

namespace Service.Iletisim.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IletisimContext>(options => options.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")));
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
