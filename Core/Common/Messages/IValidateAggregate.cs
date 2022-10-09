using System;
namespace Common.Messages
{
    public interface IValidateAggregate<TCommand, TAggregate> where TCommand : ICommand
    {
        ICommandResponse Validate(TCommand command, TAggregate aggregate);
    }
}

