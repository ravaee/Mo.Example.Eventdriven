using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo.Example.EventDriven.Common.Queue;
public interface IMessageHandler
{
    void Publish<T>(string queueName, T message) where T : IMessage;
    void Consume<T>(string queueName, Action<T> onMessage) where T : IMessage;
}
