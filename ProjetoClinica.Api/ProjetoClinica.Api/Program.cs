using ProjetoClinica.Api.Security.Cors;
using ProjetoClinica.Application;
using ProjetoClinica.Extensions.Configuration;
using ProjetoClinica.Extensions.DependencyInjection;
using ProjetoClinica.Infra;
using System.Reflection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load Configurations
        var configuration = builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                        .UseBackreference()
                        .AddEnvironmentVariables()
                        .Build();

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddCors(configuration);
        builder.Services.AddApplication();
        builder.Services.AddInfra(configuration);
        builder.Services.AddDependencies(Assembly.GetExecutingAssembly());
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}