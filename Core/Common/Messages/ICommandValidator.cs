using System;
namespace Common.Messages
{
    public interface ICommandValidator
    {
        ICommandResponse ValidateCommand<TCommand>(TCommand command) where TCommand : ICommand;
        ICommandResponse ValidateAggregate<TCommand, TAggregate>(TCommand command, TAggregate aggregate) where TCommand : ICommand;
    }
}