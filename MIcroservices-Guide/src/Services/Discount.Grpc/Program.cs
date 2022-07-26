using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Discount.Grpc.Extensions;

#region Configure services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
app.MigrateDatabase<Program>();
    
#endregion


#region Configure the HTTP request pipeline.

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run(); 

#endregion
