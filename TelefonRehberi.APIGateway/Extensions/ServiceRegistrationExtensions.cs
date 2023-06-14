
using TelefonRehberi.APIGateway.HttpClientServices.Interfaces;
using TelefonRehberi.APIGateway.HttpClientServices;

namespace Service.Kisi.Extensions
{
    public static class ServiceRegistrationExtensions
    {
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
        public static void ConfigureKisiServisiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IKisiService, KisiService>(kisi => kisi.BaseAddress = new Uri(configuration["ApiSettings:KisiUrl"]));
        }
        public static void ConfigureIletisimServisiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IIletisimService, IletisimService>(kisi => kisi.BaseAddress = new Uri(configuration["ApiSettings:IletisimUrl"]));
        }
        public static void ConfigureRaporServisiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IRaporService, RaporService>(kisi => kisi.BaseAddress = new Uri(configuration["ApiSettings:RaporUrl"]));
        }
    }
}
