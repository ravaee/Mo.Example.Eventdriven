using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Services.Email;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddSingleton<RabbitMqConnectionManager>();
builder.Services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
builder.Services.AddSingleton<IMessageConsumer, RabbitMqConsumer>();

builder.Services.AddHostedService<EmailService>();

var host = builder.Build();
host.Run();
