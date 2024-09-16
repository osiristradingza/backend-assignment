using OT.Assessment.Consumer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly IRabbitMQConsumer _rabbitMQConsumer;

        public Worker(IRabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var customerTask = _rabbitMQConsumer.ConsumeAccountQueueAsync(stoppingToken);
           //var productTask = _rabbitMQConsumer.ConsumeProductQueueAsync(stoppingToken);

            await Task.WhenAll(customerTask/*, productTask*/); // Handle both queues concurrently
        }

        public override void Dispose()
        {
            (_rabbitMQConsumer as IDisposable)?.Dispose();
            base.Dispose();
        }
    }
}
