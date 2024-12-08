using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo.Example.EventDriven.Common.Queue;
public class RabbitMQConnectionManager : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IModel Channel => _channel;

    public RabbitMQConnectionManager()
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
