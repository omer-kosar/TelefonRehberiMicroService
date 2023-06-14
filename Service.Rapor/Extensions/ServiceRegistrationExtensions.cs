using Microsoft.EntityFrameworkCore;
using Service.Kisi.Data;

namespace Service.Rapor.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RaporContext>(options => options.UseNpgsql(configuration.GetConnectionString("postgreSqlConnection")));
        }
    }
}
