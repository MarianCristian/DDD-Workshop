using System;
namespace Common.Messages
{
    public interface IHandleCommand<TCommand> where TCommand: ICommand
    {
        ICommandResponse Handle(TCommand command);
    }
}

