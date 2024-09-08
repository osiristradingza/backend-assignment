using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using OT.Assessment.Consumer.Api.Entities;
using OT.Assessment.Consumer.Api;
using Microsoft.Extensions.Configuration;

namespace OT.Assessment.Consumer
{
    public class CasinoEventPublisherService : ICasinoEventPublisherService
    {
        protected readonly IConfiguration _configuration;
        private readonly string _hostName;
        private readonly string _queueName;

        public CasinoEventPublisherService()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            _configuration = configurationBuilder.Build();
            _hostName = _configuration.GetValue<string>("RabbitHostName");
            _queueName = _configuration.GetValue<string>("RabbitQueName");
        }

        public void PublishWagerEvent(CasinoWager wagerEvent)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var message = JsonConvert.SerializeObject(wagerEvent);
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publish the message to the queue
                    channel.BasicPublish(exchange: "",
                                         routingKey: _queueName,
                                         basicProperties: null,
                                         body: body);
                }
        }
    }
}
