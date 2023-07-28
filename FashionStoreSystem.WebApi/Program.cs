using FashionStoreSystem.Infrastructure.Extensions;
using FashionStoreSystem.Services.Data.Interfaces;
using FashionStoreSystem.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreSystem.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FashionStoreDbContext>(opt => 
                opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IStatisticsService));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("FashionStoreSystem", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7189")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("FashionStoreSystem");

            app.Run();
        }
    }
}