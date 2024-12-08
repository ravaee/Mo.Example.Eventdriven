using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Mo.Example.EventDriven.Common.Queue;
public class RabbitMQManager : IMessageHandler
{
    private readonly RabbitMQConnectionManager _connectionManager;
    private readonly IModel channel;

    public RabbitMQManager(RabbitMQConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
        channel = _connectionManager.Channel;
        channel.QueueDeclare(Queues.UserRequestTicket, durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare(Queues.TicketCreated, durable: true, exclusive: false, autoDelete: false);
        channel.QueueDeclare(Queues.TicketJobDone, durable: true, exclusive: false, autoDelete: false);
    }

    public void Consume<T>(string queueName, Action<T> onMessage) where T : IMessage
    {
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));
            onMessage(message);
            Task.Delay(3000);
            channel.BasicAck(ea.DeliveryTag, false);
        };

        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
    }

    public void Publish<T>(string queueName, T message) where T : IMessage
    {
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        Task.Delay(500);
    }
}
