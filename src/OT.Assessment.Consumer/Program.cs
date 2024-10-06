using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Services;
using OT.Assessment.Consumer;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.development.json", optional: true)
            .Build();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<CasinoContext>(options => options.UseSqlServer(context.Configuration.GetConnectionString("CasinoContext"))); // Register database
        services.AddSingleton<Messages>();
        services.AddLogging(configure => configure.AddSerilog()); //log error
        services.AddHostedService<Messages>();
        services.AddSingleton<RabbitMQConnection>(sp => new RabbitMQConnection("localhost"));
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
