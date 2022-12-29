using MassTransit;
using MediatR;
using Microservices.Common.Infrastructure.MessageBrocker;
using Microservices.Common.Interfaces;
using Microservices.Items.Api.Consumers;
using Microservices.Items.BusinessLogic.MappingProfiles;
using Microservices.Items.BusinessLogic.Queries;
using Microservices.Items.DataAccessLayer;
using Microservices.Items.DataAccessLayer.Entities;
using Microservices.Items.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DtoProfile));
builder.Services.AddMediatR(typeof(GetItems).Assembly);
builder.Services.AddScoped<IRepository<Item>, ItemsRepository>();
builder.Services.AddScoped<IPublishService, PublishService>();
builder.Services.AddDbContext<ItemContext>(options =>
    options.UseSqlServer(builder.Configuration
            .GetConnectionString("ItemsDbConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);
builder.Services.AddHostedService<ItemsConsumer>();

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
