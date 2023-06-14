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
    }
}
