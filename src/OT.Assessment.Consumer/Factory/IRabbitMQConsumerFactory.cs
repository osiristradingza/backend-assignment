using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer.Factory
{
    public interface IRabbitMQConsumerFactory
    {
        Task CreateConsumerAsync(string queueName, IModel channel, Func<BasicDeliverEventArgs, Task> messageHandler, CancellationToken stoppingToken);
    }
}
