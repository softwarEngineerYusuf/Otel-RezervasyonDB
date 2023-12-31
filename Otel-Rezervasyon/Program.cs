using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using System.Text.Json.Serialization;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OtelRezervasyonDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OtelRezervasyonContext")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins", builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });

            builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
               });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyAllowSpecificOrigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}