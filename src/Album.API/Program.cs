using System.Reflection;
using System.Text;
using Album.API.API.Middlewares;
using Album.API.Application.Services;
using Album.API.Domain.Interfaces;
using Album.API.Infrastructure.Data;
using Album.API.Infrastructure.Messaging;
using Album.API.Infrastructure.Messaging.Album;
using Album.API.Infrastructure.Messaging.Background;
using Album.API.Infrastructure.Repositories;
using Album.API.Infrastructure.Repositories.Read;
using Album.API.Infrastructure.Services;
using Album.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter JWT token like: Bearer {your_token}",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                },
                Array.Empty<string>()
            }
        }
    );
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IHostedService, ServiceBackground>();
builder.Services.AddScoped<IProducerSnapshotRepository, ProducerSnapshotRepository>();
builder.Services.AddScoped<IProducerCreatedEvent, ProducerCreatedEvent>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IProducerService, ProducerService>();
builder.Services.AddScoped<IAlbumReaderRepository, AlbumReaderRepository>();
builder.Services.AddScoped<IImageStorageService, ImageStorageService>();
builder.Services.AddSingleton<IAlbumCreatedEvent, AlbumCreatedEvent>();

var jwtSettings = new JwtSettings()
{
    JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new ArgumentNullException("JWT_KEY is not set in environment variables."),
    JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new ArgumentNullException("JWT_ISSUER is not set in environment variables."),
    JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new ArgumentNullException("JWT_AUDIENCE is not set in environment variables."),
    JWT_DurationInMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_DurationInMinutes"), out var duration) ? duration : 60
};

var awsSettings = new AwsSettings
{
    AWS_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY") ?? throw new ArgumentException("AWS_ACESS_KEY is not set in environment variables"),
    AWS_SECRET_KEY = Environment.GetEnvironmentVariable("AWS_SECRET_KEY") ?? throw new ArgumentException("AWS_SECRET_KEY is not set in environment variables"),
    AWS_REGION = Environment.GetEnvironmentVariable("AWS_REGION") ?? throw new ArgumentException("AWS_REGION is not set in environment variables"),
    AWS_BUCKET_NAME = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME") ?? throw new ArgumentException("AWS_BUCKET_NAME is not set in environment variables"),
};

builder.Services.AddSingleton(awsSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.JWT_ISSUER,
        ValidAudience = jwtSettings.JWT_AUDIENCE,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JWT_KEY)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<JwtAuthMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
