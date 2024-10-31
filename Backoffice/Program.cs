using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async System.Threading.Tasks.Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Lägg till tjänster
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Konfigurera Umbraco
        builder.CreateUmbracoBuilder()
            .AddBackOffice()
            .AddWebsite()
            .AddComposers()
            .Build();

        var app = builder.Build();

        // Aktivera CORS
        app.UseCors("AllowAllOrigins");

        // Starta Umbraco
        await app.BootUmbracoAsync();

        app.UseUmbraco()
            .WithMiddleware(u =>
            {
                u.UseBackOffice();
                u.UseWebsite();
            })
            .WithEndpoints(u =>
            {
                u.UseBackOfficeEndpoints();
                u.UseWebsiteEndpoints();
            });

        // Kör appen
        await app.RunAsync();
    }
}
