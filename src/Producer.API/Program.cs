using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Producer.API.API.Middlewares;
using Producer.API.Application.Services;
using Producer.API.Domain.Interfaces;
using Producer.API.Infrastructure.Data;
using Producer.API.Infrastructure.Repositories;
using Producer.API.Infrastructure.Repositories.Read;
using Producer.API.Infrastructure.Services;
using Producer.API.Models;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

var jwtSettings = new JwtSettings()
{
    JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new ArgumentNullException("JWT_KEY is not set in environment variables."),
    JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new ArgumentNullException("JWT_ISSUER is not set in environment variables."),
    JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new ArgumentNullException("JWT_AUDIENCE is not set in environment variables."),
    JWT_DurationInMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_DURATION_IN_MINUTES"), out var duration) ? duration : 60
};

var awsSettings = new AwsSettings()
{
    AWS_ACCESS_KEY = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY") ?? throw new ArgumentNullException("AWS_ACCESS_KEY is not set in environment variables."),
    AWS_SECRET_KEY = Environment.GetEnvironmentVariable("AWS_SECRET_KEY") ?? throw new ArgumentNullException("AWS_SECRET KEY is not set in environment variables."),
    AWS_REGION = Environment.GetEnvironmentVariable("AWS_REGION") ?? throw new ArgumentNullException("AWS_REGION is not set in environment variables."),
    AWS_BUCKET_NAME = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME") ?? throw new ArgumentNullException("AWS_S3_BUCKET_NAME is not set in environment variables."),
};


builder.Services.AddSingleton(awsSettings);
builder.Services.AddSingleton(jwtSettings);

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

builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<IGetProducer, GetProducer>();
builder.Services.AddScoped<IGetProducerRepoitory, GetProducerRepository>();
builder.Services.AddScoped<IImageStorageService, ImageStorageService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
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

// Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware pipeline
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
