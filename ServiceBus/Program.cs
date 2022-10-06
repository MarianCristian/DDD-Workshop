using Azure.Messaging.ServiceBus;
using ServiceBus;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
        var serviceBusClient = new ServiceBusClient(hostContext.Configuration.GetConnectionString("ServiceBus"), clientOptions);

        services.AddSingleton(typeof(ServiceBusClient), serviceBusClient);
    })
    .Build();

await host.RunAsync();