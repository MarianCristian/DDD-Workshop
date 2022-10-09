using System;
using Common.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Messaging
{
    public class CommandValidator : ICommandValidator
    {
        private readonly IServiceProvider serviceProvider;

        public CommandValidator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommandResponse ValidateAggregate<TCommand, TAggregate>(TCommand command, TAggregate aggregate) where TCommand : ICommand
        {
            return serviceProvider.GetService<IValidateAggregate<TCommand, TAggregate>>().Validate(command, aggregate);
        }

        public ICommandResponse ValidateCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            return serviceProvider.GetService<IValidateCommand<TCommand>>().Validate(command);

        }
    }
}