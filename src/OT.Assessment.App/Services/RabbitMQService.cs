using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OT.Assessment.App.Services
{
    public class RabbitMQService
    {
        private readonly string _hostname = "localhost";
        private readonly string _queueName = "casino_wager_queue";

        public void PublishMessage<T>(T message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var messageString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(messageString);

                channel.BasicPublish(exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: body);
            }
        }       
    }
}
