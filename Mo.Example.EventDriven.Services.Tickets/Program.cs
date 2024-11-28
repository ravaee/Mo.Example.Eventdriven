using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Services.Tickets;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<TicketService>();

builder.Services.AddSingleton<RabbitMqConnectionManager>();
builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
builder.Services.AddSingleton<IMessageConsumer, RabbitMqConsumer>();

var host = builder.Build();
host.Run();
