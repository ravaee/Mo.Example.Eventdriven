using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Common.Queue;
using RabbitMQ.Client;

namespace Mo.Example.EventDriven.Services.Tickets;

public class TicketService(
    ILogger<TicketService> logger,
    IMessageHandler messageHandler) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        messageHandler.Consume<UserRequestTicketMessage>(Queues.UserRequestTicket, async message =>
        {
            var ticketId = await CreateTicketAsync(message);

            var ticketCreatedMessage = new TicketCreatedMessage
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                TicketId = ticketId,
            };

            messageHandler.Publish(Queues.TicketCreated, ticketCreatedMessage);
        });

        return Task.CompletedTask;
    }

    private Task<string> CreateTicketAsync(UserRequestTicketMessage message)
    {
        // Implement your ticket creation logic here
        return Task.FromResult("NewTicketId");
    }
}
