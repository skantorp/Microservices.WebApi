using Microservices.Common.Interfaces;
using Microservices.Inventories.BusinessLogic.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microservices.Inventories.BusinessLogic.Queries;
using Microservices.Inventories.DataAccessLayer.Entities;
using Microservices.Inventories.DataAccessLayer.Repositories;
using Microservices.Inventories.DataAccessLayer;
using MediatR;
using Microservices.Inventories.DataAccessLayer.Interfaces.Repositories;
using MassTransit;
using Microservices.Inventories.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microservices.Inventories.Api.Services.Interfaces;
using Microservices.Common.Infrastructure.MessageBrocker;
using Microservices.Inventories.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddMediatR(typeof(GetInventories).Assembly);
builder.Services.AddScoped<IPublishService, PublishService>();
builder.Services.AddScoped<IItemsHttpClient, ItemsHttpClient>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddDbContext<InventoryContext>(options =>
    options.UseSqlServer(Configuration
            .GetConnectionString("InventoryDbConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);
builder.Services.AddHttpClient("ItemsClient", config =>
{
    config.BaseAddress = new Uri(Configuration["ItemsApiLink"]);
    config.Timeout = new TimeSpan(0, 0, 30);
});
builder.Services.AddHostedService<NewItemsConsumer>();

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
