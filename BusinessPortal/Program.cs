using BusinessPortal.Data;
using BusinessPortal.EndPoint_s;
using BusinessPortal.IRepository;
using BusinessPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BusinessPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepository<Personal>, PersonalRepository>();
           


            builder.Services.AddDbContext<BusinessContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.ConfigurePersonalEndpoints();
            app.Run();
        }
    }
}