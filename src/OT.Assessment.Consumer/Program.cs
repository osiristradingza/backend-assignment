using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OT.Assessment.Consumer.Factory;
using OT.Assessment.Consumer.Interface;
using OT.Assessment.Database.Abstract;
using OT.Assessment.Database.Helper;
using OT.Assessment.Database.Interface;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create and configure the host
            var host = CreateHostBuilder(args).Build();

            // Logging when the application starts
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Application started at {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

            // Run the host (this will start consuming the RabbitMQ messages)
            await host.RunAsync();

            // Logging when the application ends
            logger.LogInformation("Application ended at {time:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
        }

        // Method to build and configure the host
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    var configuration = context.Configuration;
                    
                    services.AddLogging();
                    // Register RabbitMQ connection as a singleton
                    services.AddSingleton<IConnection>(sp =>
                    {
                        var factory = new ConnectionFactory()
                        {
                            HostName = configuration["RabbitMQ:Host"],
                            UserName = configuration["RabbitMQ:UserName"],
                            Password = configuration["RabbitMQ:Password"]
                        };
                        return factory.CreateConnection();
                    });

                    // Register RabbitMQ channels for account and wager processing as singletons
                    services.AddSingleton(sp =>
                    {
                        var connection = sp.GetRequiredService<IConnection>();
                        return connection.CreateModel(); // Account Channel
                    });

                    services.AddSingleton(sp =>
                    {
                        var connection = sp.GetRequiredService<IConnection>();
                        return connection.CreateModel(); // Wager Channel
                    });

                    // Register Database Connection using Dapper and provide the connection string from appsettings.json
                    services.AddSingleton<IDatabaseConnection>(sp =>
                        new DatabaseConnection(configuration.GetConnectionString("DatabaseConnection")));

                    // Register IAccounts repository to interact with the database via Dapper
                    services.AddSingleton<IAccounts, AccountsRepository>();
                    services.AddSingleton<IWagers, WagersRepository>();

                    // Register the RabbitMQ consumer service
                    services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumerService>();
                    services.AddSingleton<IRabbitMQConsumerFactory, RabbitMQConsumerFactory>();

                    // Add the background worker that will consume messages from the queues
                    services.AddHostedService<Worker>();
                });
    }
}
