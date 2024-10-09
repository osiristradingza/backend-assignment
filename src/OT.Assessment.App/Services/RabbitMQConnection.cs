using RabbitMQ.Client;

namespace OT.Assessment.App.Services
{
    public class RabbitMQConnection
    {
        private readonly string _hostname;
        private IConnection _connection;

        public RabbitMQConnection(string hostname)
        {
            _hostname = hostname;
            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    UserName = "guest",
                    Password = "guest",
                    RequestedHeartbeat = TimeSpan.FromSeconds(60),
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create RabbitMQ connection: {ex.Message}");
                _connection = null;
            }
        }

        public IConnection GetConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                Console.WriteLine("RabbitMQ connection is not open.");
                return null; 
            }
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
