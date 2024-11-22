using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using WebApplication2.Data;
using WebApplication2.Repositories;

namespace WebApplication2
{
    class Program
    {
        public static void Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>( options => options.UseInMemoryDatabase("EmployeeDb"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>  
                {
                    builder.WithOrigins("http://localhost:4200", "http://localhost:58435")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            
            // add the emlpoyee repository to the DI (dependency injection)
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("CORS");

            app.MapControllers();

            app.Run();

        }
    }
}
