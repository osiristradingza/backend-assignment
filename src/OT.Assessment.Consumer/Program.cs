using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Interface;
using OT.Assessment.Consumer.Service;
using OT.Assessment.Consumer;
using OT.Assessment.Database.Helper;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Database.Interface;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    })
    .ConfigureServices((context, services) =>
    {
        //configure services
        var configuration = context.Configuration;

        // Register the RabbitMQ consumer service and Dapper repositories
        services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumerService>();

        // Register with Dapper
        services.AddSingleton<IAccounts, AccountsRepository>();

        // Configure SQL Connection from appsettings.json
        services.AddSingleton<IConfiguration>(configuration);

        // Register the worker service that consumes both queues
        services.AddHostedService<Worker>();

    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application started {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

await host.RunAsync();

logger.LogInformation("Application ended {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);