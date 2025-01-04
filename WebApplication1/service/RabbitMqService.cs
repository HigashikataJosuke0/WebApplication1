namespace WebApplication1.setvice;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqService
{
    private readonly string _queueName = "user-registrations";
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqService(string uri)
    {
        var factory = new ConnectionFactory { Uri = new Uri(uri) };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    public void Publish<T>(T message)
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        _channel.BasicPublish(
            exchange: "",
            routingKey: _queueName,
            basicProperties: null,
            body: body
        );
    }
}