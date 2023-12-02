using EFCoreMovies.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EFCoreMovies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    //lazy loading configuration
                    //options.UseLazyLoadingProxies();
                    options.UseSqlServer("name=DefaultConnection", sqlserver => sqlserver.UseNetTopologySuite());
                    //options.UseModel(ApplicationDbContextModel.Instance);
                });
            //builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddScoped<IUserService, UserServiceFake>();
            builder.Services.AddScoped<IChangeTrackerEventHandler, ChangeTrackerEventHandler>();
            builder.Services.AddSingleton<Singleton>();

            var app = builder.Build();

            //executing pending migration on application start - will slow down the application loading time
            //using(var scope = app.Services.CreateScope())
            //{
            //    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    applicationDbContext.Database.Migrate();
            //}

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