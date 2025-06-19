using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Product.API.Domain.Interfaces;
using Product.API.Infrastructure.Data;
using Product.API.Infrastructure.Messaging;
using Product.API.Infrastructure.Messaging.Backgrorund;
using Product.API.Infrastructure.Repositories.Write;

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


builder.Services.AddSingleton<IHostedService, ServiceBackground>();
builder.Services.AddScoped<IAlbumProductRepository, AlbumProductRepository>();
builder.Services.AddScoped<IAlbumCreatedEvent, AlbumCreatedEvent>();

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

