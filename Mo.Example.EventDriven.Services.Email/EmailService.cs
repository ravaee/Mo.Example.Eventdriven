using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Common.Queue;

namespace Mo.Example.EventDriven.Services.Email;

public class EmailService(IMessageHandler messageHandler) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        messageHandler.Consume<TicketCreatedMessage>(Queues.TicketCreated, message =>
        {
            messageHandler.Publish(Queues.TicketJobDone, new EmailSentMessage());
        });

        return Task.CompletedTask;
    }
}
