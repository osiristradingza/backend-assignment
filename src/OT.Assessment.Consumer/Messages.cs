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
        private readonly CasinoWagerService _casinoWagerService;

        public Messages(RabbitMQConnection rabbitMqConnection, IServiceProvider serviceProvider, ILogger<Messages> logger, CasinoWagerService casinoWagerService)
        {
            _rabbitMqConnection = rabbitMqConnection;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _casinoWagerService = casinoWagerService;
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
                        await _casinoWagerService.AddCasinoWagerAsync(wager);
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
    }
}
