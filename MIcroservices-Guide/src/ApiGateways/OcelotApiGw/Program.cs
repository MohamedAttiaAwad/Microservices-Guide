using Ocelot;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


#region Services
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging((hostingContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("loging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());
var app = builder.Build();
#endregion



#region 

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();
app.Run();

#endregion