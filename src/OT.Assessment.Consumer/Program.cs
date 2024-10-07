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
            .AddJsonFile("appsettings.development.json", optional: true);
    })
    .ConfigureServices((context, services) =>
    {
        // Register database context
        services.AddDbContext<CasinoContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("CasinoContext")));

        services.AddLogging(configure => configure.AddSerilog()); // Log error

        // Register RabbitMQ services
        services.AddSingleton<RabbitMQConnection>(sp => new RabbitMQConnection("localhost"));
        services.AddScoped<CasinoWagerService>(); 
        services.AddSingleton<RabbitMQService>(); 
        services.AddHostedService<RabbitMQConsumerService>(); // Register RabbitMQConsumerService as Hosted Service
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
