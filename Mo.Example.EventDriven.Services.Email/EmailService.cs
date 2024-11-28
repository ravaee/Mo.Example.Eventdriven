using Mo.Example.EventDriven.Common;

namespace Mo.Example.EventDriven.Services.Email;

public class EmailService(
    IMessageConsumer consumer,
    IMessagePublisher publisher) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //consumer.Consume<TicketCreatedMessage>("TicketCreated", async message =>
        //{
        //    publisher.Publish("TicketJobDone", new EmailSentMessage());
        //});

        return Task.CompletedTask;
    }
}
