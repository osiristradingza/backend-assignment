using Microsoft.Extensions.Configuration;
using OT.Assessment.Consumer.Interface;
using OT.Assessment.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer.Service
{
    public class RabbitMQConsumerService : IRabbitMQConsumer
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

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection(); ;
            _accountChannel = _connection.CreateModel();
            _wagerChannel = _connection.CreateModel();
        }

        // Consume the account queue asynchronously
        public Task ConsumeAccountQueueAsync(CancellationToken stoppingToken)
        {
            try
            {
                _accountChannel.QueueDeclare(queue: Queues.AccountQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new AsyncEventingBasicConsumer(_accountChannel); // Use Async consumer for RabbitMQ
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    try
                    {
                        var account = JsonSerializer.Deserialize<AddAccountRequest>(message);

                        //using (var scope = _serviceProvider.CreateScope())
                        //{
                        //    var customerRepository = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();
                        //    await customerRepository.AddCustomerAsync(customer); // Async DB operation
                        //}

                        _logger.LogInformation($"Account {account.FirstName} saved to the database.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error processing customer message: {ex.Message}");
                    }
                };

                _accountChannel.BasicConsume(queue: Queues.AccountQueue, autoAck: true, consumer: consumer);

                return Task.CompletedTask;
            }
            catch (Exception ex) 
            {
                return Task.CompletedTask;
            }
        }

        // Consume the Product queue asynchronously
        //public Task ConsumeProductQueueAsync(CancellationToken stoppingToken)
        //{
        //    _wagerChannel.QueueDeclare(queue: "products_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        //    var consumer = new AsyncEventingBasicConsumer(_wagerChannel); // Use Async consumer for RabbitMQ
        //    consumer.Received += async (model, ea) =>
        //    {
        //        var body = ea.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);

        //        try
        //        {
        //            var product = JsonSerializer.Deserialize<Product>(message);

        //            using (var scope = _serviceProvider.CreateScope())
        //            {
        //                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
        //                await productRepository.AddProductAsync(product); // Async DB operation
        //            }

        //            _logger.LogInformation($"Product {product.Name} saved to the database.");
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError($"Error processing product message: {ex.Message}");
        //        }
        //    };

        //    _wagerChannel.BasicConsume(queue: "products_queue", autoAck: true, consumer: consumer);

        //    return Task.CompletedTask;
        //}

        public void Dispose()
        {
            _accountChannel.Close();
            _wagerChannel.Close();
            _connection.Close();
        }
    }
}
