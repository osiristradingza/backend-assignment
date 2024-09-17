using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer.Factory
{
    public class RabbitMQConsumerFactory : IRabbitMQConsumerFactory
    {
        private readonly ILogger<RabbitMQConsumerFactory> _logger;

        public RabbitMQConsumerFactory(ILogger<RabbitMQConsumerFactory> logger)
        {
            _logger = logger;
        }

        public Task CreateConsumerAsync(string queueName, IModel channel, Func<BasicDeliverEventArgs, Task> messageHandler, CancellationToken stoppingToken)
        {
            try
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    _logger.LogInformation($"Received a message from the queue: {queueName}");
                    await messageHandler(ea);
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                _logger.LogInformation($"Consumer for {queueName} queue initiated.");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error initializing queue consumption: {ex.Message}");
                return Task.CompletedTask;
            }
        }
    }
}
