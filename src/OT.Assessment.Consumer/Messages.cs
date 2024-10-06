using OT.Assessment.App.Models.Casino;
using OT.Assessment.App.Models.DTOs;
using OT.Assessment.App.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace OT.Assessment.Consumer
{
    public class Messages : BackgroundService
    {
        private readonly RabbitMQConnection _rabbitMqConnection;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<Messages> _logger;

        public Messages(RabbitMQConnection rabbitMqConnection, IServiceProvider serviceProvider, ILogger<Messages> logger)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var channel = _rabbitMqConnection.GetConnection().CreateModel();
            channel.QueueDeclare(queue: "casino_wager_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var wager = JsonSerializer.Deserialize<CasinoWagerPackage>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var casinoContext = scope.ServiceProvider.GetRequiredService<CasinoContext>();
                    try
                    {
                        await SaveWagerAsync(casinoContext, wager);
                        _logger.LogInformation("Successfully saved wager: {WagerId}", wager.wagerId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while saving wager: {WagerId}", wager.wagerId);
                    }
                }
            };

            channel.BasicConsume(queue: "casino_wager_queue", autoAck: false, consumer: consumer);
            await Task.CompletedTask;
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
    }
}
