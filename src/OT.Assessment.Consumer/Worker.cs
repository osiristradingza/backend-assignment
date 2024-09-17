using OT.Assessment.Consumer.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

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
            await Task.WhenAll(
                _rabbitMQConsumer.ConsumeAccountQueueAsync(stoppingToken),
                _rabbitMQConsumer.ConsumeCountryQueueAsync(stoppingToken),
                _rabbitMQConsumer.ConsumeWagerQueueAsync(stoppingToken)
            );
        }

        public override void Dispose()
        {
            (_rabbitMQConsumer as IDisposable)?.Dispose();
            base.Dispose();
        }
    }
}
