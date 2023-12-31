using Service.Kisi.Extensions;

namespace TelefonRehberi.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.ConfigureRabbitMqProducerService();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureVersioning();
            builder.Services.ConfigureVersionedApiExplorer();
            builder.Services.ConfigureKisiServisiHttpClient(builder.Configuration);
            builder.Services.ConfigureIletisimServisiHttpClient(builder.Configuration);
            builder.Services.ConfigureRaporServisiHttpClient(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}