using System;
using System.Reflection;
using MessagingSystem.GrpcService;
using MessagingSystem.GrpcService.Data;
using MessagingSystem.GrpcService.Providers;
using MessagingSystem.GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MappingProfiles>();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=Messages.db"));

builder.Services.AddAutoMapper(typeof(MappingProfiles));

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddGrpc();  //.AddJsonTranscoding();
var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<MessageService>();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
