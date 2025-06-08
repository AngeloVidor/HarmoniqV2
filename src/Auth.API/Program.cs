using System.Reflection;
using Auth.API.Application.Security;
using Auth.API.Domain.Interfaces;
using Auth.API.Infrastructure.Data;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtConfiguration, JwtConfiguration>();

var jwtSettings = new JwtSettings()
{
    JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new ArgumentNullException("JWT_KEY is not set in environment variables."),
    JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new ArgumentNullException("JWT_ISSUER is not set in environment variables."),
    JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new ArgumentNullException("JWT_AUDIENCE is not set in environment variables."),
    JWT_DurationInMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_DURATION_IN_MINUTES"), out var duration) ? duration : 60
};

builder.Services.AddSingleton(jwtSettings);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapControllers();

app.Run();

