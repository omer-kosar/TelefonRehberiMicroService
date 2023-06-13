using FluentValidation.AspNetCore;
using Kisi.Service.Extensions;
using Kisi.Service.Filters;
using Microsoft.AspNetCore.Mvc;
using Service.Kisi.Extensions;
using System.Reflection;

namespace Service.Kisi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            builder.Services.AddScoped<ValidationFilter>();
            builder.Services.Configure<ApiBehaviorOptions>(options
                => options.SuppressModelStateInvalidFilter = true); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigurePostgreSqlContext(builder.Configuration);
            builder.Services.ConfigureRepository();
            builder.Services.ConfigureVersioning();
            builder.Services.ConfigureVersionedApiExplorer();
            var app = builder.Build().MigrateDatabase();

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