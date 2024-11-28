using Mo.Example.EventDriven.Common;
using RabbitMQ.Client;

namespace Mo.Example.EventDriven.Services.Tickets;

public class TicketService(
    ILogger<TicketService> logger,
    IMessageConsumer consumer, 
    IMessagePublisher publisher) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Consume<UserRequestTicketMessage>("UserRequestTicket", async message =>
        {
            var ticketId = await CreateTicketAsync(message);

            var ticketCreatedMessage = new TicketCreatedMessage
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                TicketId = ticketId,
            };

            publisher.Publish("TicketCreated", ticketCreatedMessage);
        });

        return Task.CompletedTask;
    }

    private Task<string> CreateTicketAsync(UserRequestTicketMessage message)
    {
        // Implement your ticket creation logic here
        return Task.FromResult("NewTicketId");
    }
}
