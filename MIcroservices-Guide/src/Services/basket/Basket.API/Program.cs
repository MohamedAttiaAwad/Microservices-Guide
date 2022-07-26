using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


#region Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigurationManager configuration = builder.Configuration;
//add redis configration
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
});


builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option =>
    option.Address = new Uri(configuration["GrpcSettings:DiscountUrl"])
) ;
builder.Services.AddScoped<DiscountGrpcService>();


// General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build(); 
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
