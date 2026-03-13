using RabbitMQ.Client;
using System.Threading.Channels;

namespace ImageResizer
{
    public class RabbitMQPublisher
    {
        private readonly IConfiguration _configuration;
        private IConnection? _connection;
        private IChannel? _channel;
        public RabbitMQPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OpenConenction()
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.UserName = _configuration["RabbitMQ:Username"];
            factory.Password = _configuration["RabbitMQ:Password"];
            factory.VirtualHost = _configuration["RabbitMQ:VirtualHost"];
            factory.HostName = _configuration["RabbitMQ:Hostname"];

            _connection = await factory.CreateConnectionAsync();
        }

        public async Task DisposeConnection()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
            await _channel.DisposeAsync();
            await _connection.DisposeAsync();
        }
       
    }
}
