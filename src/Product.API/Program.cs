using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Product.API.Domain.Interfaces;
using Product.API.Infrastructure.Data;
using Product.API.Infrastructure.Messaging;
using Product.API.Infrastructure.Messaging.Backgrorund;
using Product.API.Infrastructure.Repositories.Write;
using Product.API.Infrastructure.Services;
using Product.API.Models;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var stripe = new StripeSettings()
{
    SecretKey = Environment.GetEnvironmentVariable("Secret_Key") ?? throw new InvalidOperationException("Secret_Key environment variable is not set."),
    PublishableKey = Environment.GetEnvironmentVariable("Publishable_Key") ?? throw new InvalidOperationException("Publishable_Key environment variable is not set.")
};

builder.Services.AddSingleton(stripe);

builder.Services.AddSingleton<IHostedService, ServiceBackground>();
builder.Services.AddScoped<IAlbumProductRepository, AlbumProductRepository>();
builder.Services.AddScoped<IAlbumCreatedEvent, AlbumCreatedEvent>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton(new Stripe.ProductService());
builder.Services.AddSingleton(new Stripe.PriceService());


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

