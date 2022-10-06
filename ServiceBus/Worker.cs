using System.Reflection;
using Azure.Messaging.ServiceBus;
using Common.Messages;

namespace ServiceBus;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ServiceBusClient serviceBusClient;
    private readonly IServiceProvider serviceProvider;

    public Worker(ILogger<Worker> logger, ServiceBusClient serviceBusClient, IServiceProvider serviceProvider)
    {
        _logger = logger;
        this.serviceBusClient = serviceBusClient;
        this.serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queues = new string[] { };
        var topics = new string[] { };

        await SubscribeToQueues(queues);
        await SubscribeToTopics(topics);
    }

    private async Task SubscribeToTopics(string[] topics)
    {
        foreach (var topic in topics)
        {
            var processor = serviceBusClient.CreateProcessor(topic, "Workshop", new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }
    }
    private async Task SubscribeToQueues(string[] queues)
    {
        foreach (var queue in queues)
        {
            var processor = serviceBusClient.CreateProcessor(queue, new ServiceBusProcessorOptions());

            processor.ProcessMessageAsync += QueuedMessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            await processor.StartProcessingAsync();
        }
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var messageType = Type.GetType(args.Message.ContentType);

        var method = typeof(Worker).GetMethod(nameof(HandleEvent), BindingFlags.NonPublic | BindingFlags.Instance);
        method.MakeGenericMethod(messageType).Invoke(this, new object[] { args.Message });

        await args.CompleteMessageAsync(args.Message);
    }
    private async Task QueuedMessageHandler(ProcessMessageEventArgs args)
    {
        var messageType = Type.GetType(args.Message.ContentType);

        var method = typeof(Worker).GetMethod(nameof(HandleQueuedEvent), BindingFlags.NonPublic | BindingFlags.Instance);
        method.MakeGenericMethod(messageType).Invoke(this, new object[] { args.Message });

        await args.CompleteMessageAsync(args.Message);
    }

    private void HandleEvent<TEvent>(ServiceBusReceivedMessage message) where TEvent : IDomainEvent
    {
        var eventHandlers = serviceProvider.GetServices<IEventHandler<TEvent>>();
        Parallel.ForEach(eventHandlers, eventHandler =>
        {
            eventHandler.Handle(message.Body.ToObjectFromJson<TEvent>());
        });
    }
    private void HandleQueuedEvent<TEvent>(ServiceBusReceivedMessage message) where TEvent : IQueuedDomainEvent
    {
        var eventHandlers = serviceProvider.GetServices<IQueuedEventHandler<TEvent>>();
        Parallel.ForEach(eventHandlers, eventHandler =>
        {
            eventHandler.Handle(message.Body.ToObjectFromJson<TEvent>());
        });
    }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        throw args.Exception;
    }
}

