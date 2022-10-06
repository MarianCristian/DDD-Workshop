using System;
using Common.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Messaging
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommandResponse Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            return serviceProvider.GetService<IHandleCommand<TCommand>>().Handle(command);
        }
    }
}
