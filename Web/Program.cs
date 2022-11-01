using Common.Messages;
using Infrastructure.Messaging;
using Azure.Messaging.ServiceBus;
using Common.Factories;
using Infrastructure.Data.TableStorage;
using Infrastructure.Services;
using Common.Services;
using Infrastructure.Data.QueryRepository;
using Azure.Data.Tables;
using Infrastructure.Data.SQL;
using Microsoft.EntityFrameworkCore;
using AccountManagement.Events;
using AccountManagement.EventHandlers;
using AccountManagement.Commands;
using AccountManagement.CommandHandlers;
using AccountManagement.Queries;
using AccountManagement.Projections;
using AccountManagement.QueryHandlers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        RegisterInfrastructureDependencies(builder);
        RegisterServiceBusClient(builder);
        RegisterTableStorageClient(builder);
        RegisterSQLServerClient(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void RegisterInfrastructureDependencies(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        builder.Services.AddScoped<IEventPublisher, EventPublisher>();
        builder.Services.AddScoped<IMessageBroker, ServiceBusEventPublisher>();
        builder.Services.AddScoped<ITSContext, TSContext>();
        builder.Services.AddScoped<IEventStore, EventStore>();
        builder.Services.AddScoped<IDomainEventConversionService, DomainEventConversionService>();
        builder.Services.AddScoped<ICommandValidator, CommandValidator>();

        builder.Services.AddScoped<IEventHandler<BankAccountRegistered>, BankAccountEventHandler>();
        builder.Services.AddScoped<IEventHandler<BankAccountRegistered>, NotificationEventHandler>();
        builder.Services.AddScoped<IEventHandler<TransactionInitiated>, TransactionEventHandler>();
        builder.Services.AddScoped<IEventHandler<TransactionInitiated>, NotificationEventHandler>();
        builder.Services.AddScoped<IEventHandler<TransactionInitiated>, BankAccountEventHandler>();

        builder.Services.AddScoped<IHandleCommand<RegisterBankAccountCommand>, BankAccountCommandHandler>();
        builder.Services.AddScoped<IHandleCommand<InitiateTransactionCommand>, BankAccountCommandHandler>();

        builder.Services.AddScoped<IHandleQuery<BankAccountQuery, BankAccountProjection>, BankAccountQueryHandler>();
    }

    private static void RegisterSQLServerClient(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<WorkshopDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
    }

    private static void RegisterTableStorageClient(WebApplicationBuilder builder)
    {
        var tableServiceClient = new TableServiceClient(builder.Configuration.GetConnectionString("TableStorage"));
        builder.Services.AddSingleton(typeof(TableServiceClient), tableServiceClient);
    }

    private static void RegisterServiceBusClient(WebApplicationBuilder builder)
    {
        var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpWebSockets };
        var serviceBusClient = new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"), clientOptions);
        builder.Services.AddSingleton(typeof(ServiceBusClient), serviceBusClient);
    }
}