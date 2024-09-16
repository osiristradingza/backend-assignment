using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Consumer.Interface
{
    public interface IRabbitMQConsumer
    {
        Task ConsumeAccountQueueAsync(CancellationToken stoppingToken);
    }
}
