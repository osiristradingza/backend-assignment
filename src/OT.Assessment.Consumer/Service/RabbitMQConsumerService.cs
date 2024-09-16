using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OT.Assessment.Consumer.Interface;
using OT.Assessment.Database.Interface;
using OT.Assessment.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer.Service
{
    public class RabbitMQConsumerService : IRabbitMQConsumer, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQConsumerService> _logger;
        private readonly IConnection _connection;
        private readonly IModel _accountChannel;
        private readonly IModel _wagerChannel;

        public RabbitMQConsumerService(IServiceProvider serviceProvider, ILogger<RabbitMQConsumerService> logger, IConnection connection, IModel accountChannel, IModel wagerChannel)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _connection = connection;
            _accountChannel = accountChannel;
            _wagerChannel = wagerChannel;
        }

        public Task ConsumeAccountQueueAsync(CancellationToken stoppingToken)
        {
            try
            {
                _accountChannel.QueueDeclare(queue: Queues.AccountQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new AsyncEventingBasicConsumer(_accountChannel);
                _logger.LogInformation("Consumer for account queue initiated.");

                consumer.Received += async (model, ea) =>
                {
                    _logger.LogInformation($"Received a message from the queue: {Queues.AccountQueue}");

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($"Message content: {message}");

                    try
                    {
                        var account = JsonSerializer.Deserialize<AddAccountRequest>(message);

                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var accountManager = scope.ServiceProvider.GetRequiredService<IAccounts>();
                            await accountManager.AddAccountAsync(account);
                        }

                        _logger.LogInformation($"Account {account.FirstName} saved to the database.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing message: {ex.Message}");
                    }
                };

                _accountChannel.BasicConsume(queue: Queues.AccountQueue, autoAck: true, consumer: consumer);

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error initializing queue consumption: {ex.Message}");
                return Task.CompletedTask;
            }
        }

        public void Dispose()
        {
            _accountChannel?.Close();
            _wagerChannel?.Close();
            _connection?.Close();
        }
    }
}
