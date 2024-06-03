using Microsoft.EntityFrameworkCore;
using PlantCare.API.BusinessLogic;
using PlantCare.API.Models.Context;

namespace PlantCare.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure the database context to use SQL Server
            builder.Services.AddDbContext<PlantCareContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlantCareContext") ?? throw new InvalidOperationException("Connection string 'PlantCareContext' not found.")));

            // Add services to the container.
            builder.Services.AddScoped<PlantCareService>();

            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
