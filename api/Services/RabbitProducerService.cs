using System.Text;
using api.Interfaces;
using RabbitMQ.Client;
using shared_library.Models;

public class RabbitProducerService : IProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitProducerService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    // Dispose method to clean up resources
    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }

    public async Task Produce(CompletedText message)
    {
        var body = Encoding.UTF8.GetBytes(message.ToString());

        _channel.BasicPublish(exchange: string.Empty, routingKey: "hello",
            basicProperties: null, body: body);
          
        Console.WriteLine($" [x] Sent {message}");
    }
}