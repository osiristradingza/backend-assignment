using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Models.DTOs;

namespace OT.Assessment.App.Services
{
    public class RabbitMQConsumerService : IHostedService
    {
        private readonly RabbitMQConnection _rabbitMqConnection;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQConsumerService> _logger;
        private IModel _channel;
        private readonly string _queueName = "casino_wager_queue";

        public RabbitMQConsumerService(RabbitMQConnection rabbitMqConnection, IServiceProvider serviceProvider, ILogger<RabbitMQConsumerService> logger)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _channel = _rabbitMqConnection.GetConnection().CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ Consumer started, waiting for messages...");
            StartConsuming();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            return Task.CompletedTask;
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Message received: {Message}", message);

                var casinoWager = JsonSerializer.Deserialize<CasinoWagerPackage>(message);

                if (casinoWager == null)
                {
                    _logger.LogWarning("Failed to deserialize message: {Message}", message);
                    _channel.BasicAck(ea.DeliveryTag, false); // Acknowledge to ignore this message
                    return;
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var casinoWagerService = scope.ServiceProvider.GetRequiredService<CasinoWagerService>();
                    try
                    {
                        await casinoWagerService.AddCasinoWagerAsync(casinoWager);
                        _channel.BasicAck(ea.DeliveryTag, false); // Acknowledge the message after processing
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to save wager: {WagerId}", casinoWager.wagerId);
                        _channel.BasicNack(ea.DeliveryTag, false, true); // Requeue the message
                    }
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
    }
}
