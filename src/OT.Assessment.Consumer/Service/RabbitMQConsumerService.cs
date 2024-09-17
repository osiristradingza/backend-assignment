using OT.Assessment.Consumer.Factory;
using OT.Assessment.Consumer.Interface;
using OT.Assessment.Database.Interface;
using OT.Assessment.Model;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMQConsumerService : IRabbitMQConsumer, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMQConsumerService> _logger;
    private readonly IRabbitMQConsumerFactory _consumerFactory;
    private readonly IConnection _connection;
    private readonly IModel _accountChannel;
    private readonly IModel _wagerChannel;
    private readonly IModel _countryChannel;

    public RabbitMQConsumerService(
        IServiceProvider serviceProvider,
        ILogger<RabbitMQConsumerService> logger,
        IRabbitMQConsumerFactory consumerFactory,
        IConnection connection,
        IModel accountChannel,
        IModel wagerChannel,
        IModel countryChannel)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _consumerFactory = consumerFactory;
        _connection = connection;
        _accountChannel = accountChannel;
        _wagerChannel = wagerChannel;
        _countryChannel = countryChannel;
    }

    public Task ConsumeAccountQueueAsync(CancellationToken stoppingToken)
    {
        return _consumerFactory.CreateConsumerAsync(Queues.AccountQueue, _accountChannel, async ea =>
        {
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
        }, stoppingToken);
    }

    public Task ConsumeCountryQueueAsync(CancellationToken stoppingToken)
    {
        return _consumerFactory.CreateConsumerAsync(Queues.CountryQueue, _countryChannel, async ea =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Message content: {message}");

            try
            {
                var country = JsonSerializer.Deserialize<AddCountryRequest>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var accountManager = scope.ServiceProvider.GetRequiredService<IAccounts>();
                    await accountManager.AddCountryAsync(country);
                }

                _logger.LogInformation($"Country {country.CountryCode} saved to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
            }
        }, stoppingToken);
    }

    public Task ConsumeWagerQueueAsync(CancellationToken stoppingToken)
    {
        return _consumerFactory.CreateConsumerAsync(Queues.WagerQueue, _wagerChannel, async ea =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Message content: {message}");

            try
            {
                var wager = JsonSerializer.Deserialize<AddCasinoWagerRequest>(message);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var wagerManager = scope.ServiceProvider.GetRequiredService<IWagers>();
                    await wagerManager.PlayerWagerAsync(wager);
                }

                _logger.LogInformation($"Wager for {wager.Username} saved to the database.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing message: {ex.Message}");
            }
        }, stoppingToken);
    }

    public void Dispose()
    {
        _accountChannel?.Close();
        _wagerChannel?.Close();
        _countryChannel?.Close();
        _connection?.Close();
    }
}
