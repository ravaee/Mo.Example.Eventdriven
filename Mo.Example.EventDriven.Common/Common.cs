using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Mo.Example.EventDriven.Common;

//Models
public interface IMessage
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}

public class UserRequestTicketMessage : IMessage
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
    // Other properties
}

public class TicketCreatedMessage : IMessage
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string TicketId { get; set; }
    // Other properties
}

public class EmailSentMessage : IMessage
{
    public Guid Id { get; }
    public DateTime Timestamp { get; }
}

public class RabbitMqConnectionManager : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IModel Channel => _channel;

    public RabbitMqConnectionManager()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            if (_connection == null)
            {
                _connection = factory.CreateConnection();

            }
            _channel = _connection.CreateModel();

        }
        catch (Exception ex)
        {


        }

    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}

public interface IMessagePublisher
{
    void Publish<T>(string queueName, T message) where T : IMessage;
}

public interface IMessageConsumer
{
    void Consume<T>(string queueName, Action<T> onMessage) where T : IMessage;
}

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqConnectionManager _connectionManager;

    public RabbitMqPublisher(RabbitMqConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public void Publish<T>(string queueName, T message) where T : IMessage
    {
        var channel = _connectionManager.Channel;

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }
}

public class RabbitMqConsumer : IMessageConsumer
{
    private readonly RabbitMqConnectionManager _connectionManager;

    public RabbitMqConsumer(RabbitMqConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public void Consume<T>(string queueName, Action<T> onMessage) where T : IMessage
    {
        var channel = _connectionManager.Channel;

        channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
            onMessage(message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}


