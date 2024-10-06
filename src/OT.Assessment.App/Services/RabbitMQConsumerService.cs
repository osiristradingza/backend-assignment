using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Models.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace OT.Assessment.App.Services
{
    public class RabbitMQConsumerService
    {
        private readonly RabbitMQConnection _rabbitMqConnection;
        private readonly string _queueName = "casino_wager_queue";
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQConsumerService> _logger;
        private IModel _channel;

        public RabbitMQConsumerService(RabbitMQConnection rabbitMqConnection, IServiceProvider serviceProvider, ILogger<RabbitMQConsumerService> logger)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _channel = _rabbitMqConnection.GetConnection().CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _logger.LogInformation("RabbitMQ Consumer started, waiting for messages...");
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
                    var casinoContext = scope.ServiceProvider.GetRequiredService<CasinoContext>();
                    try
                    {
                        await SaveWagerAsync(casinoContext, casinoWager);
                        _channel.BasicAck(ea.DeliveryTag, false); // Acknowledge the message after publishing
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

        private async Task SaveWagerAsync(CasinoContext casinoContext, CasinoWagerPackage wager)
        {
            var players = new Players
            {
                PlayerId = Guid.NewGuid(),
                Username = wager.username
            };

            casinoContext.Players.Add(players);
            await casinoContext.SaveChangesAsync();
            _logger.LogInformation("New player created: {Username}", wager.username);

            var casinoWager = new CasinoWagers
            {
                WagerId = Guid.NewGuid(),
                Theme = wager.theme,
                Provider = wager.provider,
                GameName = wager.gameName,
                TransactionId = Guid.NewGuid(),
                BrandId = Guid.NewGuid(),
                AccountId = Guid.NewGuid(),
                Username = wager.username,
                ExternalReferenceId = Guid.NewGuid(),
                TransactionTypeId = Guid.NewGuid(),
                Amount = (decimal)wager.amount,
                CreatedDateTime = wager.createdDateTime,
                NumberOfBets = wager.numberOfBets,
                CountryCode = wager.countryCode,
                SessionData = wager.sessionData,
                Duration = wager.duration,
                PlayerId = players.PlayerId
            };

            await casinoContext.CasinoWagers.AddAsync(casinoWager);
            await casinoContext.SaveChangesAsync();
        }

        public void StopConsuming()
        {
            _channel?.Close();
        }
    }
}
