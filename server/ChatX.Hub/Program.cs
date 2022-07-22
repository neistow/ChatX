using System.Reflection;
using ChatX.Application.Commands;
using ChatX.Application.Events;
using ChatX.Hub.HubFilters;
using ChatX.Hub.Hubs;
using ChatX.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddInfrastructure(configuration);

services.AddFluentValidation();
services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CancelActiveConversationCommand).Assembly);

services.AddSignalR(opt => { opt.AddFilter<FluentValidationFilter>(); });

services.AddCors(opt => opt
    .AddDefaultPolicy(pb =>
        pb.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    ));

var app = builder.Build();

app.UseCors();
app.MapHub<ChatHub>("/chat");
app.MapHub<ChatStatisticsHub>("/stats");

app.Run();