using WebApplication1.Controllers;
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var configuration = builder.Configuration;
            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllers();
            var temp1 = configuration["Demo:Key1"];
            Console.WriteLine(temp1);
            builder.Services.Configure<UrlOptions>(configuration.GetSection(UrlOptions.UrlKey));
            builder.Services.Configure<JwtConfigurationOptions>(configuration.GetSection(JwtConfigurationOptions.JwtKey));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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