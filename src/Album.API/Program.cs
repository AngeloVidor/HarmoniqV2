using Album.API.Domain.Interfaces;
using Album.API.Infrastructure.Data;
using Album.API.Infrastructure.Messaging;
using Album.API.Infrastructure.Messaging.Background;
using Album.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IHostedService, ServiceBackground>();
builder.Services.AddScoped<IProducerSnapshotRepository, ProducerSnapshotRepository>();
builder.Services.AddScoped<IProducerCreatedEvent, ProducerCreatedEvent>();


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
