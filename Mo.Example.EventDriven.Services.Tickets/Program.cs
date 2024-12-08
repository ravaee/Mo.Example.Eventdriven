using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Common.Queue;
using Mo.Example.EventDriven.Services.Tickets;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TicketService>();

builder.Services.AddSingleton<RabbitMQConnectionManager>();
builder.Services.AddSingleton<IMessageHandler, RabbitMQManager>();

var host = builder.Build();
host.Run();
