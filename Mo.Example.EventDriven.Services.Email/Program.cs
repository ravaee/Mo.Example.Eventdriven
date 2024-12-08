using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Common.Queue;
using Mo.Example.EventDriven.Services.Email;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<RabbitMQConnectionManager>();
builder.Services.AddSingleton<IMessageHandler, RabbitMQManager>();

builder.Services.AddHostedService<EmailService>();

var host = builder.Build();
host.Run();
