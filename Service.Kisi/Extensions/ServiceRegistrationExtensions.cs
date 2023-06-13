using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;

namespace Service.Kisi.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KisiContext>(options => options.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")));
        }
    }
}
