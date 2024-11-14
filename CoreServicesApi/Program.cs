using Models;
using Services;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using EndPoints;
using Microsoft.AspNetCore.Authentication.Cookies;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", corsBuilder =>
            {
                corsBuilder.WithOrigins("http://localhost:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
            });
        });

        builder.Services.AddSingleton(new TokenService("Bqo8t7cGmLCSCrk+rqdBwkKsNBiObMhIUYgawiQ5irOlVbi2OxhLXaFfmdNn8Tt4WwRcF2ggCLjACyuuZupd3kdXMKPTt0vKnMUqZuvW5UHUg4+KtwAWIANoDlkJrs2qi04GH+M/C57LRUOjTDFOsgtlvHUzw5m9GIGpMJ1QF2s="));
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie.SameSite = SameSiteMode.None; 
                options.Cookie.HttpOnly = true; 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IUseService, UserService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("AllowAll");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.MapUserEndpoints();
        app.Run();
    }
}
